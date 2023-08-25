using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.Services.Interfaces;
using LiquorStoreFinalProject.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LiquorStoreFinalProject.Controllers
{
    public class Products : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public Products(AppDbContext context, IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(int page=1, string searchTerm = null)
        {
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;
            ViewBag.Recent3Blogs = _context.Blogs.Take(3).OrderByDescending(b => b.CreatedDate).ToList();

            var data = await _productService.GetPaginatedProductsAsync(page,searchTerm);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product is null) return NotFound();

            ProductDetailVM productDetailVM = new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageURL = product.ImageURL,
                CategoryId = product.CategoryId,
                Price = product.Price,
            };

            return View(productDetailVM);
        }
        [HttpGet]
        public async Task<IActionResult> FilterProductsByCategory(int categoryId,int page=1)
        {
            var categories =  _context.Categories;
            ViewBag.Categories = categories;
            ViewBag.Recent3Blogs = _context.Blogs.Take(3).OrderByDescending(b => b.CreatedDate).ToList();
            var filteredProducts= await _productService.GetProductsByCategory(page, categoryId);

            return View("GetAllProducts",filteredProducts);
        }
    }
}
    