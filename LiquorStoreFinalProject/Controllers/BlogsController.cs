using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreFinalProject.Controllers
{
    public class BlogsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        public BlogsController(AppDbContext context, IBlogService blogService)
        {
            _context = context;
            _blogService = blogService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var blogs = await _blogService.GetPaginatedDatasAsync(page);
            return View(blogs);
        }
        public async Task<IActionResult> BlogDetails(int id)
        {
            ViewBag.Recent3Blogs = _context.Blogs.Take(3).OrderByDescending(b => b.CreatedDate).ToList();
            var blog =  _context.Blogs.FirstOrDefault(b => b.Id == id);
            return View(blog);
        }

    }
}
