using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiquorStoreFinalProject.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product is null) return NotFound();

            ProductDetailVM productDetailVM = new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ProductImage = product.ProductImage,
                CategoryName = product.Category.Name,
                ActualPrice = product.Price,
                DiscountPrice = product.Price - 10,
                Percent=10,
            };

            return View(productDetailVM);
        }
    }
}
