namespace LiquorStoreFinalProject.Areas.Admin.ViewModels.Category
{
    public class UpdateCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageURL { get; set; }
        public IFormFile? Image { get; set; }
    }
}
