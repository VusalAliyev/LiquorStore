using LiquorStoreFinalProject.Areas.Admin.ViewModels.Category;
using LiquorStoreFinalProject.Areas.Admin.ViewModels.Product;
using LiquorStoreFinalProject.Models;

namespace LiquorStoreFinalProject.Services.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int id);
        Task CreateAsync(CreateCategoryVM createCategoryVM);
        Task UpdateAsync(UpdateCategoryVM updateCategoryVM);
        Task DeleteAsync(int id);
    }
}
