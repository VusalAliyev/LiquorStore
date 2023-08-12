using LiquorStoreFinalProject.Models;

namespace LiquorStoreFinalProject.Services.Interfaces
{
    public interface IDiscountService
    {
        List<Discount> GetAll();
        Discount GetById(int id);
    }
}
