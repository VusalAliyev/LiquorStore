    using LiquorStoreFinalProject.Areas.Admin.ViewModels.Product;
using LiquorStoreFinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreFinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page=1)
        {
            var products = await _productService.GetPaginatedProductsAsync(page);
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductVM request)
        {
            await _productService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }
    }
}
