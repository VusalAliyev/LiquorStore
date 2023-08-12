using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.Services.Interfaces;

namespace LiquorStoreFinalProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }
        public Category GetById(int id)
        {
            var selectedCategory=_context.Categories.FirstOrDefault(c => c.Id == id);
            return selectedCategory;
        }
    }
}
