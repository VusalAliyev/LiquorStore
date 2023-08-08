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
        private readonly IWebHostEnvironment _env;
        private const long MaxFileSizeBytes = 10 * 1024 * 1024; // 10 MB
        private static readonly string[] AllowedMimeTypes = { "image/jpeg", "image/png" };
        public ProductService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(CreateProductVM createProductVM)
        {
            var FileUniqueName = createProductVM.Image.FileName;

            var folderPath = Path.Combine(_env.WebRootPath, "photos");
            var FullPath = Path.Combine(folderPath, FileUniqueName);
            string rootFolder = @"wwwroot\";
            string returnPath = FullPath.Substring(FullPath.IndexOf(rootFolder, StringComparison.OrdinalIgnoreCase) + rootFolder.Length).Replace("\\", "/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            Directory.CreateDirectory(folderPath);

            using (FileStream fs = new FileStream(FullPath, FileMode.Create))
            {
                createProductVM.Image.CopyTo(fs);
            }

            var product = new Product
            {
                Name = createProductVM.Name,
                CategoryId = 3,
                Description = createProductVM.Description,
                Price = createProductVM.Price,
                DiscountId = 1,
                ImageURL = returnPath,
            };
            _context.Products.Add(product);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var deletedProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
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

        public async Task<GetPaginatedProductVM> GetProductsByCategory(int page, int categoryId)
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

        public async Task UpdateAsync(int productId, UpdateProductVM updateProductVM)
        {
            var updatedProduct = _context.Products.FirstOrDefault(p => p.Id == productId);

            var FileUniqueName = updateProductVM.Image.FileName;

            var folderPath = Path.Combine(_env.WebRootPath, "photos");
            var FullPath = Path.Combine(folderPath, FileUniqueName);
            string rootFolder = @"wwwroot\";
            string returnPath = FullPath.Substring(FullPath.IndexOf(rootFolder, StringComparison.OrdinalIgnoreCase) + rootFolder.Length).Replace("\\", "/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            Directory.CreateDirectory(folderPath);

            using (FileStream fs = new FileStream(FullPath, FileMode.Create))
            {
                updateProductVM.Image.CopyTo(fs);
            }

            updatedProduct.Name = updateProductVM.Name;
            updatedProduct.CategoryId=updateProductVM.CategoryId;
            updatedProduct.ImageURL = returnPath;
            updatedProduct.Description=updateProductVM.Description;
            updatedProduct.Price=updateProductVM.Price;

             _context.SaveChanges();
        }
    }
}
