using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.Services.Interfaces;
using LiquorStoreFinalProject.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace LiquorStoreFinalProject.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GetPaginatedProductVM> GetPaginatedDatasAsync(int page)
        {

            var pageResults = 3f;
            var pageCount = Math.Ceiling(_context.Products.Count() / pageResults);

            var products = await _context.Products.Select(p => new GetAllProductVM
             {
                 Id = p.Id,
                 ImageURL = p.ImageURL,
                 CategoryName = p.Category.Name,
                 Name = p.Name,
                 Price = p.Price,
             }).Skip((page - 1) * (int)pageResults)
               .Take((int)pageResults)
               .ToListAsync();

            return new GetPaginatedProductVM
            {
                CurrentPage = page,
                Products = products,
                Pages = (int)pageCount
            };

        }

        Task<IEnumerable<GetAllProductVM>> IProductService.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
