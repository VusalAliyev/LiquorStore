using LiquorStoreFinalProject.Helpers;
using LiquorStoreFinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace LiquorStoreFinalProject.Controllers
{
    [Authorize(Roles = "User,Admin") ]
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {

            if (HttpContext.Session.TryGetValue("TotalPrice", out byte[] totalPriceBytes))
            {
                double totalPriceDouble = BitConverter.ToDouble(totalPriceBytes, 0); 
                decimal totalPrice = Convert.ToDecimal(totalPriceDouble); 
                ViewBag.TotalPrice = totalPrice;
            }
            if (HttpContext.Session.TryGetValue("TotalDiscount", out byte[] totalDiscountsBytes))
            {
                double totalDiscountDouble = BitConverter.ToDouble(totalDiscountsBytes, 0);
                decimal totalDiscount = Convert.ToDecimal(totalDiscountDouble);
                ViewBag.TotalDiscount = totalDiscount;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Index(PaymentVM paymentVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            return RedirectToAction(nameof(PaymentSuccess));
        }
        [HttpGet]
        public IActionResult PaymentSuccess()
        {
            List<BasketVM> emptyBasket = new List<BasketVM>();
            string emptyCookies = JsonConvert.SerializeObject(emptyBasket);
            Response.Cookies.Append("basket", emptyCookies, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });

            return View();
        }
    }
}
