using LiquorStoreFinalProject.Models;

namespace LiquorStoreFinalProject.Services.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int id);
    }
}
