using LiquorStoreFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LiquorStoreFinalProject.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.TryGetValue("TotalPrice", out byte[] totalPriceBytes))
            {
                double totalPriceDouble = BitConverter.ToDouble(totalPriceBytes, 0); // Byte dizisini double'a dönüştür
                decimal totalPrice = Convert.ToDecimal(totalPriceDouble); // Double'ı decimal'a çevir
                ViewBag.TotalPrice = totalPrice;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Index(PaymentVM paymentVM)
        {
            return RedirectToAction(nameof(PaymentSuccess));
        }
        [HttpGet]
        public IActionResult PaymentSuccess()
        {
            return View();
        }
    }
}
