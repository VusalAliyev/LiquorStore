using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LiquorStoreFinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _context.Products;
            IEnumerable<Category> categories = _context.Categories;
            IEnumerable<Blog> blogs = _context.Blogs.Take(4).OrderByDescending(b=>b.CreatedDate);
            HomeVM homeVM = new HomeVM()
            {
                Blogs = blogs,
                Categories = categories,
                Products = products
            };

            return View(homeVM);
        }
    }
}