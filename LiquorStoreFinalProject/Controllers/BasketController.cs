using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.Helpers;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.Services;
using LiquorStoreFinalProject.Services.Interfaces;
using LiquorStoreFinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cms.Ecc;

namespace LiquorStoreFinalProject.Controllers
{
    [Authorize(Roles = "User,Admin")]
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
                    Count = 1,
                    DiscountId= product.DiscountId,
                };
                basketProducts.Add(newBasketProduct);
            }




            string cookies = JsonConvert.SerializeObject(basketProducts);
            Response.Cookies.Append("basket", cookies, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });

            return RedirectToAction("Index");
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
                products = new List<BasketVM>();
            }

            decimal totalPrice = 0;
            decimal totalDiscount = 0;
            foreach (BasketVM item in products)
            {
                var selectedDiscount = _context.Discounts.FirstOrDefault(d => d.Id == item.DiscountId);
                Product product = await _context.Products.FindAsync(item.Id);

                item.Description = product.Description;
                item.Name = product.Name;
                item.Price = product.Price;
                item.DiscountName = selectedDiscount.Name;
                item.ImageURL=product.ImageURL;
                var itemDiscount = (item.Price * selectedDiscount.Percent) / 100 ;


                totalDiscount += itemDiscount;
                totalPrice += item.Count * product.Price;
                
            }
            byte[] totalPriceBytes = BitConverter.GetBytes(decimal.ToDouble(totalPrice)); // Decimal veriyi double'a dönüştürüp byte dizisi olarak sakla
            HttpContext.Session.Set("TotalPrice", totalPriceBytes);

            byte[] totalDiscountsBytes = BitConverter.GetBytes(decimal.ToDouble(totalDiscount)); // Decimal veriyi double'a dönüştürüp byte dizisi olarak sakla
            HttpContext.Session.Set("TotalDiscount", totalDiscountsBytes);


            ViewBag.TotalDiscount= totalDiscount;

            return View(products);
        }

        public IActionResult RemoveFromBasket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string cookies = Request.Cookies["basket"];

            if (cookies != null)
            {
                List<BasketVM> basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(cookies);

                BasketVM productToRemove = basketProducts.FirstOrDefault(p => p.Id == id);

                if (productToRemove != null)
                {
                    basketProducts.Remove(productToRemove);

                    // Güncellenmiş ürün listesini tekrar cookie'e kaydet
                    string updatedCookies = JsonConvert.SerializeObject(basketProducts);
                    Response.Cookies.Append("basket", updatedCookies, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult ClearBasket()
        {
            List<BasketVM> emptyBasket = new List<BasketVM>();
            string emptyCookies = JsonConvert.SerializeObject(emptyBasket);
            Response.Cookies.Append("basket", emptyCookies, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });

            return RedirectToAction("Index");
        }
    }
}
