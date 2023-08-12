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

    }
}
