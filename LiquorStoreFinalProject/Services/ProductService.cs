using LiquorStoreFinalProject.Areas.Admin.ViewModels.Product;
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
        private const long MaxFileSizeBytes = 10 * 1024 * 1024; // 10 MB
        private static readonly string[] AllowedMimeTypes = { "image/jpeg", "image/png"};
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateProductVM createProductVM)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(createProductVM.Image.FileName);

            var directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "images");


            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await createProductVM.Image.CopyToAsync(fileStream);
            }

            var product = new Product
            {
                Name= createProductVM.Name,
                CategoryId= 3,
                Description= createProductVM.Description,
                DiscountId= createProductVM.DiscountId,
                ImageURL=filePath,
            };
            _context.Products.Add(product);

            await _context.SaveChangesAsync(); 
        }

        public async Task DeleteAsync(int id)
        {
            var deletedProduct=await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            _context.Products.Remove(deletedProduct);

            _context.SaveChanges();
        }

        public async Task<GetPaginatedProductVM> GetPaginatedProductsAsync(int page)
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

        public async Task<GetPaginatedProductVM> GetProdutsByCategory(int page,int categoryId)
        {
            var pageResults = 3f;
            var pageCount = Math.Ceiling(_context.Products.Count() / pageResults);

            var filteredProducts = await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new GetAllProductVM
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
                Products = filteredProducts,
                Pages = (int)pageCount
            };
        }
        

        
    }
}
