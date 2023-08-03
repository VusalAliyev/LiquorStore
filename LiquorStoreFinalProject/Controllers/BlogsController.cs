using LiquorStoreFinalProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreFinalProject.Controllers
{
    public class BlogsController : Controller
    {
        private readonly AppDbContext _context;

        public BlogsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = _context.Blogs.ToList();
            return View(blogs);
        }
        public async Task<IActionResult> BlogDetails(int id)
        {
            var blog =  _context.Blogs.FirstOrDefault(b => b.Id == id);
            return View(blog);
        }
    }
}
