using LiquorStoreFinalProject.Areas.Admin.ViewModels.Category;
using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.Services;
using LiquorStoreFinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreFinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryVM createCategoryVM)
        {
           await _categoryService.CreateAsync(createCategoryVM);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            
            Category category = _categoryService.GetById(id);
            UpdateCategoryVM updateCategoryVM = new UpdateCategoryVM()
            {
                Id = category.Id,
                Name = category.Name,
                ImageURL=category.ImageURL
            };
            return View(updateCategoryVM);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryVM request)
        {
            await _categoryService.UpdateAsync(request);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory (int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
