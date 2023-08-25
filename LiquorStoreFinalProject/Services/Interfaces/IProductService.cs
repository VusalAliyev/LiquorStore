using LiquorStoreFinalProject.Areas.Admin.ViewModels.Product;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.ViewModels;

namespace LiquorStoreFinalProject.Services.Interfaces
{
    public interface IProductService
    {
        Task<GetPaginatedProductVM> GetPaginatedProductsAsync(int page, string searchTerm = null);
        Task<GetPaginatedProductVM> GetProductsByCategory(int page,int categoryId);
        Task CreateAsync(CreateProductVM createProductVM);
        Task UpdateAsync(UpdateProductVM updateProductVM);
        Task DeleteAsync(int id);
        Product GetProductById(int id);
    }
}
