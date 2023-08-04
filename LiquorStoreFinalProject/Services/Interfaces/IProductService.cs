using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.ViewModels;

namespace LiquorStoreFinalProject.Services.Interfaces
{
    public interface IProductService
    {
        Task<GetPaginatedProductVM> GetPaginatedProductsAsync(int page);
        Task<GetPaginatedProductVM> GetProdutsByCategory(int page,int categoryId);
    }
}
