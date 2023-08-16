using LiquorStoreFinalProject.Models;

namespace LiquorStoreFinalProject.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
