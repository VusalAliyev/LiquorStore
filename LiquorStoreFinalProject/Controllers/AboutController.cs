using LiquorStoreFinalProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreFinalProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Categories=_context.Categories;
            return View();
        }
    }
}
