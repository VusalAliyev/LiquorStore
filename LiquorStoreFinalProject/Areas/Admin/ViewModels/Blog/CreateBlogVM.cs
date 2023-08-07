namespace LiquorStoreFinalProject.Areas.Admin.ViewModels.Blog
{
    public class CreateBlogVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
