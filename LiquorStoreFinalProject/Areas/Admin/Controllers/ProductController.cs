using AspNetCore;
using LiquorStoreFinalProject.Areas.Admin.ViewModels.Product;
using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreFinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var product = _productService.GetProductById(id);
            return View(product);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductVM request)
        {
            await _productService.UpdateAsync(request);
            return View(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
