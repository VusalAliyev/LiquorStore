//using AspNetCore;
using LiquorStoreFinalProject.Areas.Admin.ViewModels.Blog;
using LiquorStoreFinalProject.Areas.Admin.ViewModels.Product;
using LiquorStoreFinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreFinalProject.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        public IActionResult Index(int page=1)
        {
            var blogs = _blogService.GetPaginatedDatasAsync(page);
            return View(blogs);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateBlogVM createBlogVM)
        {
            _blogService.CreateAsync(createBlogVM);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPut]
        public IActionResult Update(int blogId, UpdateBlogVM updateBlogVM)
        {
            _blogService.UpdateAsync(blogId, updateBlogVM);
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _blogService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
