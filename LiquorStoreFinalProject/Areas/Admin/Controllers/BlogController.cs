using LiquorStoreFinalProject.Areas.Admin.ViewModels.Blog;
using LiquorStoreFinalProject.Helpers;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.Services;
using LiquorStoreFinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiquorStoreFinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var blogs = await _blogService.GetPaginatedDatasAsync(page);
            return View(blogs);
        }
        [HttpGet]
        public async Task<IActionResult> CreateBlog()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogVM request)
        {
            await _blogService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }

        //Update
        [HttpGet]
        public async Task<IActionResult> UpdateBlog(int id)
        {
            Blog blog = _blogService.GetBlogById(id);
            UpdateBlogVM updateBlogVM = new UpdateBlogVM()
            {
                Id=blog.Id,
                Description=blog.Description,
                ImageURL=blog.ImageURL,
                Title=blog.Title,
            };
            return View(updateBlogVM);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBlog(UpdateBlogVM request)
        {
            await _blogService.UpdateAsync(request);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _blogService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
