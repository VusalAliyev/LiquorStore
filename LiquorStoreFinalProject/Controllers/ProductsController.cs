using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LiquorStoreFinalProject.Controllers
{
    public class Products : Controller
    {
        private readonly AppDbContext _context;

        public Products(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {

            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;
            var products = await _context.Products.Select(p => new GetAllProductVM
            {
                Id = p.Id,
                ProductImageId = p.ProductImageId,
                CategoryName = p.Category.Name,
                Name = p.Name,
                Price = p.Price,
            }).ToListAsync();

            return View(products);
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
                ProductImageId = product.ProductImageId,
                CategoryId = product.CategoryId,
                Price = product.Price,
            };

            return View(productDetailVM);
        }
        [HttpGet]
        public async Task<IActionResult> FilterProductsByCategory(int categoryId)
        {
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;

            var filteredProducts = await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new GetAllProductVM
                {
                    Id = p.Id,
                    ProductImageId = p.ProductImageId,
                    CategoryName = p.Category.Name,
                    Name = p.Name,
                    Price = p.Price
                }).ToListAsync();

            return View("GetAllProducts",filteredProducts);
        }
    }
}
