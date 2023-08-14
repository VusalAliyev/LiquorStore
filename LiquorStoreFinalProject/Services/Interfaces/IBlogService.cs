using LiquorStoreFinalProject.Areas.Admin.ViewModels.Blog;
using LiquorStoreFinalProject.Areas.Admin.ViewModels.Product;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.ViewModels;

namespace LiquorStoreFinalProject.Services.Interfaces
{
    public interface IBlogService
    {
        Task<GetPaginatedBlogVM> GetPaginatedDatasAsync(int page);
        Task<GetPaginatedBlogVM> GetDatasByCategory(int page, int categoryId);
        Task CreateAsync(CreateBlogVM createBlogVM);
        Task UpdateAsync(UpdateBlogVM updateBlogVM);
        Task DeleteAsync(int id);
        Blog GetBlogById(int id);

    }
}
