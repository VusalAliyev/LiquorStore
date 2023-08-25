using LiquorStoreFinalProject.Areas.Admin.ViewModels.Blog;
using LiquorStoreFinalProject.Areas.Admin.ViewModels.Category;
using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.Services.Interfaces;

namespace LiquorStoreFinalProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(CreateCategoryVM createCategoryVM)
        {
            var FileUniqueName = createCategoryVM.Image.FileName;

            var folderPath = Path.Combine(_env.WebRootPath, "photos");
            var FullPath = Path.Combine(folderPath, FileUniqueName);
            string rootFolder = @"wwwroot\";
            string returnPath = FullPath.Substring(FullPath.IndexOf(rootFolder, StringComparison.OrdinalIgnoreCase) + rootFolder.Length).Replace("\\", "/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            Directory.CreateDirectory(folderPath);

            using (FileStream fs = new FileStream(FullPath, FileMode.Create))
            {
                createCategoryVM.Image.CopyTo(fs);
            }
            var category = new Category
            {
                Name = createCategoryVM.Name,
                ImageURL = returnPath
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var deletedCategory = _context.Categories.FirstOrDefault(c => c.Id == id);
            _context.Categories.Remove(deletedCategory);
            await _context.SaveChangesAsync();
        }

        public List<Category> GetAll()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }
        public Category GetById(int id)
        {
            var selectedCategory = _context.Categories.FirstOrDefault(c => c.Id == id);
            return selectedCategory;
        }
        public async Task UpdateAsync(UpdateCategoryVM updateCategoryVM)
        {
            var updatedCategory = _context.Categories.FirstOrDefault(p => p.Id == updateCategoryVM.Id);
            if (updateCategoryVM.ImageURL != null)
            {
                var FileUniqueName = updateCategoryVM.Image.FileName;

                var folderPath = Path.Combine(_env.WebRootPath, "photos");
                var FullPath = Path.Combine(folderPath, FileUniqueName);
                string rootFolder = @"wwwroot\";
                string returnPath = FullPath.Substring(FullPath.IndexOf(rootFolder, StringComparison.OrdinalIgnoreCase) + rootFolder.Length).Replace("\\", "/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                Directory.CreateDirectory(folderPath);

                using (FileStream fs = new FileStream(FullPath, FileMode.Create))
                {
                    updateCategoryVM.Image.CopyTo(fs);
                }
            updatedCategory.ImageURL = returnPath;
            }

            updatedCategory.Name = updateCategoryVM.Name;
            _context.SaveChanges();
        }
    }
}
