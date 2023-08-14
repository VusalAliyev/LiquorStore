namespace LiquorStoreFinalProject.Areas.Admin.ViewModels.Blog
{
    public class UpdateBlogVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageURL { get; set; }
        public IFormFile Image { get; set; }
    }
}
