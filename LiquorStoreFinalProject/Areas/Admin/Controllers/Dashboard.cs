using LiquorStoreFinalProject.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreFinalProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
