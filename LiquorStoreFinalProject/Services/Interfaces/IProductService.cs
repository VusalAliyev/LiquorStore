using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.ViewModels;

namespace LiquorStoreFinalProject.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<GetAllProductVM>> GetAllAsync();
        Task<GetPaginatedProductVM> GetPaginatedDatasAsync(int page);
    }
}
