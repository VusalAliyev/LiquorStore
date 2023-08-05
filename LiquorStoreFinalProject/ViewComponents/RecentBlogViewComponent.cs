using LiquorStoreFinalProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreFinalProject.ViewComponents
{
    public class RecentBlogsViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public RecentBlogsViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var recentBlogs=_context.Blogs
                .OrderByDescending(b => b.CreatedDate)
                .Take(3);
            return View(recentBlogs);
        }

    }
}
