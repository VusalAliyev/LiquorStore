using LiquorStoreFinalProject.Areas.Admin.ViewModels.Category;
using LiquorStoreFinalProject.Areas.Admin.ViewModels.Discount;
using LiquorStoreFinalProject.Models;

namespace LiquorStoreFinalProject.Services.Interfaces
{
    public interface IDiscountService
    {
        List<Discount> GetAll();
        Discount GetById(int id);
        Task CreateAsync(CreateDiscountVM createDiscountVM);
        Task UpdateAsync(UpdateDiscountVM updateDiscountVM);
        Task DeleteAsync(int id);
    }
}
