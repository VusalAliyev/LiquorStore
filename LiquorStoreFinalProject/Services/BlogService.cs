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

        public BlogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateBlogVM createBlogVM)
        {

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(createBlogVM.Image.FileName);


            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BlogImages", uniqueFileName);


            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, uniqueFileName); 

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await createBlogVM.Image.CopyToAsync(fileStream);
            }

            var createdBlog = new Blog
            {
                Title= createBlogVM.Title,
                Description=createBlogVM.Description,
                Image=filePath
            };
             _context.Blogs.Add(createdBlog);

            _context.SaveChanges();
        }



        public async Task DeleteAsync(int id)
        {
            var deletedBlog=_context.Blogs.FirstOrDefault(b => b.Id == id);
            _context.Blogs.Remove(deletedBlog);
            _context.SaveChanges();
        }

        public Task<GetPaginatedBlogVM> GetDatasByCategory(int page, int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<GetPaginatedBlogVM> GetPaginatedDatasAsync(int page)
        {
            var pageResults = 3f;
            var pageCount = Math.Ceiling(_context.Products.Count() / pageResults);

            var blogs = await _context.Blogs.Select(p => new GetAllBlogVM
            {
              Description = p.Description,
              Image=p.Image,
              Title=p.Title
            }).Skip((page - 1) * (int)pageResults)
               .Take((int)pageResults)
               .ToListAsync();

            return new GetPaginatedBlogVM
            {
                CurrentPage = page,
                Blogs= blogs,
                Pages = (int)pageCount
            };
        }

        public async Task UpdateAsync(int blogId, UpdateBlogVM updateBlogVM)
        {
            var updatedBlog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == blogId);

            if (updatedBlog == null)
            {
                throw new Exception("Blog not found");
            }

            if (System.IO.File.Exists(updatedBlog.Image))
            {
                System.IO.File.Delete(updatedBlog.Image);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(updateBlogVM.Image.FileName);

            //FerrumCapital.API\bin\Debug\net7.0\Downloaded\
            var directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BlogImages");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await updateBlogVM.Image.CopyToAsync(fileStream);
            }

            updatedBlog.Title = updateBlogVM.Title;
            updatedBlog.Description = updateBlogVM.Description;
            updatedBlog.Image = filePath;


            await _context.SaveChangesAsync();

        }
    }
}
