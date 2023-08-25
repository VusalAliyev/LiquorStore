using LiquorStoreFinalProject.Areas.Admin.ViewModels.Blog;
using LiquorStoreFinalProject.Areas.Admin.ViewModels.Product;
using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.Services.Interfaces;
using LiquorStoreFinalProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Threading;

namespace LiquorStoreFinalProject.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(CreateBlogVM createBlogVM)
        {
            var FileUniqueName = createBlogVM.Image.FileName;

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
                createBlogVM.Image.CopyTo(fs);
            }

            var createdBlog = new Blog
            {
                Title = createBlogVM.Title,
                Description = createBlogVM.Description,
                ImageURL = returnPath
            };
            _context.Blogs.Add(createdBlog);

            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var deletedBlog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            _context.Blogs.Remove(deletedBlog);
            _context.SaveChanges();
        }

        public Blog GetBlogById(int id)
        {
            var selectedBlog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            return selectedBlog;
        }

        public Task<GetPaginatedBlogVM> GetDatasByCategory(int page, int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<GetPaginatedBlogVM> GetPaginatedDatasAsync(int page)
        {
            var pageResults = 4f;
            var pageCount = Math.Ceiling(_context.Blogs.Count() / pageResults);

            var blogs = await _context.Blogs.Select(p => new GetAllBlogVM
            {
                Id = p.Id,
                CreateDate = p.CreatedDate,
                Description = p.Description,
                ImageURL = p.ImageURL,
                Title = p.Title
            }).Skip((page - 1) * (int)pageResults)
               .Take((int)pageResults)
               .ToListAsync();

            return new GetPaginatedBlogVM
            {
                CurrentPage = page,
                Blogs = blogs,
                Pages = (int)pageCount
            };
        }

        public async Task UpdateAsync(UpdateBlogVM updateBlogVM)
        {
            var updatedBlog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == updateBlogVM.Id);

            if (updatedBlog == null)
            {
                throw new Exception("Blog not found");
            }
            if (updateBlogVM.ImageURL!=null)
            {
                if (System.IO.File.Exists(updatedBlog.ImageURL))
                {
                    System.IO.File.Delete(updatedBlog.ImageURL);
                }
                var FileUniqueName = updateBlogVM.Image.FileName;

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
                    updateBlogVM.Image.CopyTo(fs);
                }
                updatedBlog.ImageURL = returnPath;
            }
            
           

            updatedBlog.Title = updateBlogVM.Title;
            updatedBlog.Description = updateBlogVM.Description;
           

            await _context.SaveChangesAsync();

        }
    }
}
