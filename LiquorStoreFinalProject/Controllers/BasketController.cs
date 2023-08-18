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
            Product product = _context.Products.FirstOrDefault(p => p.Id == id);

            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(Request.Cookies["basket"]);
            products.Add(product);




            string cookies = JsonConvert.SerializeObject(products);
            Response.Cookies.Append("basket", cookies, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });

            return Content("ok" + id);
        }

        public ActionResult Basket()
        {
            string cookies = Request.Cookies["basket"];

            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(cookies);

            return Json(products);
        }

    }
}
