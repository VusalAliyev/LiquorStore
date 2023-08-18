using LiquorStoreFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreFinalProject.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
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
