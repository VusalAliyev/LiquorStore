using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreFinalProject.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
