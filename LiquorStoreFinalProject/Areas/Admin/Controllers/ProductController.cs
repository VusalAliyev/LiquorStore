using LiquorStoreFinalProject.Areas.Admin.ViewModels.Product;
using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.Helpers;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreFinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;

        public ProductController(IProductService productService, AppDbContext context)
        {
            _productService = productService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var products = await _productService.GetPaginatedProductsAsync(page);
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Discounts= _context.Discounts.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductVM request)
        {
            await _productService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Discounts = _context.Discounts.ToList();

            Product product = _productService.GetProductById(id);
            UpdateProductVM updateProductVM = new UpdateProductVM()
            {
                CategoryId = product.CategoryId,
                Name = product.Name,
                Description= product.Description,
                DiscountId= product.DiscountId,
                Id= product.Id,
                ImageURL=product.ImageURL,
                Price=product.Price
            };
            return View(updateProductVM);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductVM request)
        {
            await _productService.UpdateAsync(request);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
