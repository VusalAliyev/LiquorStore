using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.Services;
using LiquorStoreFinalProject.Services.Interfaces;
using LiquorStoreFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cms.Ecc;

namespace LiquorStoreFinalProject.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        public BasketController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null) return NotFound();
            Product product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            List<BasketVM> basketProducts;
            if (Request.Cookies["basket"] == null)
            {
                basketProducts = new List<BasketVM>();
            }
            else
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }


            BasketVM existBasketProduct = basketProducts.FirstOrDefault(p => p.Id == id);

            if (existBasketProduct != null)
            {
                existBasketProduct.Count++;
            }
            else
            {
                BasketVM newBasketProduct = new BasketVM
                {
                    Id = product.Id,
                    Count = 1
                };
                basketProducts.Add(newBasketProduct);
            }




            string cookies = JsonConvert.SerializeObject(basketProducts);
            Response.Cookies.Append("basket", cookies, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });

            return Content("ok" + id);
        }

        public async Task<ActionResult> Index()
        {
            string cookies = Request.Cookies["basket"];

            List<BasketVM> products;
            
            if (cookies != null)
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(cookies);
            }
            else
            {
                products=new List<BasketVM>();
            }
            foreach (BasketVM item in products)
            {
                Product product = await _context.Products.FindAsync(item.Id);

                item.Description = product.Description;
                item.Name = product.Name;
                item.Price = product.Price;
            }

            return View(products);
        }

    }
}
