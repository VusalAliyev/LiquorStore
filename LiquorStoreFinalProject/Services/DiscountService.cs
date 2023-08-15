using LiquorStoreFinalProject.Areas.Admin.ViewModels.Discount;
using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LiquorStoreFinalProject.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly AppDbContext _context;

        public DiscountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateDiscountVM createDiscountVM)
        {
            var createdDiscount = new Discount()
            {
                Name = createDiscountVM.Name,
                Percent = createDiscountVM.Percent
            };
            _context.Discounts.Add(createdDiscount);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var deletedDiscount = _context.Discounts.FirstOrDefault(d => d.Id == id);
            _context.Discounts.Remove(deletedDiscount);
            _context.SaveChanges();
        }

        public List<Discount> GetAll()
        {
            var discounts = _context.Discounts.ToList();
            return discounts;
        }
        public Discount GetById(int id)
        {
            var selectedDiscount = _context.Discounts.FirstOrDefault(d => d.Id == id);
            return selectedDiscount;
        }

        public async Task UpdateAsync(UpdateDiscountVM updateDiscountVM)
        {
            var updatedDiscount = _context.Discounts.FirstOrDefault(d => d.Id == updateDiscountVM.Id);

            updatedDiscount.Name=updateDiscountVM.Name;
            updatedDiscount.Percent=updateDiscountVM.Percent;
            _context.SaveChanges();
        }
    }
}
