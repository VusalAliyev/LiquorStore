using LiquorStoreFinalProject.Areas.Admin.ViewModels.Category;
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
        private readonly ICategoryService _categoryService;
        private readonly IDiscountService _discountService;
        public ProductService(AppDbContext context, IWebHostEnvironment env, ICategoryService categoryService, IDiscountService discountService)
        {
            _context = context;
            _env = env;
            _categoryService = categoryService;
            _discountService = discountService;
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

            var selectedCategory = _categoryService.GetById(createProductVM.CategoryId);
            var selectedDiscount = _discountService.GetById(createProductVM.DiscountId);

            var product = new Product
            {
                Name = createProductVM.Name,
                Discount = selectedDiscount,
                CategoryId = selectedCategory.Id,
                Description = createProductVM.Description,
                Price = createProductVM.Price,
                DiscountId = selectedDiscount.Id,
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
        public Product GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            return product;
        }
        public async Task<GetPaginatedProductVM> GetPaginatedProductsAsync(int page, string searchTerm = null)
        {

            var pageResults = 6f;

            IQueryable<Product> productsQuery = _context.Products;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchTerm));
            }

            var pageCount = Math.Ceiling(productsQuery.Count() / pageResults);

            var products = await productsQuery
                .Select(p => new GetAllProductVM
                {
                    Id = p.Id,
                    ImageURL = p.ImageURL,
                    CategoryName = p.Category.Name,
                    DiscountName = p.Discount.Name,
                    Name = p.Name,
                    Price = p.Price,
                })
                .Skip((page - 1) * (int)pageResults)
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
            var pageResults = 6f;
            var pageCount = Math.Ceiling(_context.Products.Where(x => x.CategoryId == categoryId).Count() / pageResults);

            var filteredProducts = await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new GetAllProductVM
                {
                    Id = p.Id,
                    ImageURL = p.ImageURL,
                    CategoryName = p.Category.Name,
                    DiscountName = p.Discount.Name,
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

        public async Task UpdateAsync(UpdateProductVM updateProductVM)
        {
            var updatedProduct = _context.Products.FirstOrDefault(p => p.Id == updateProductVM.Id);

            if (updateProductVM.ImageURL != null)
            {
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
                updatedProduct.ImageURL = returnPath;   
            }


            updatedProduct.Name = updateProductVM.Name ?? updatedProduct.Name;
            updatedProduct.CategoryId =updateProductVM.CategoryId;
            updatedProduct.DiscountId = updateProductVM.DiscountId;
            updatedProduct.Description = updateProductVM.Description;
            updatedProduct.Price = updateProductVM.Price;
            _context.Products.Update(updatedProduct);
            _context.SaveChanges();
        }
    }
}
