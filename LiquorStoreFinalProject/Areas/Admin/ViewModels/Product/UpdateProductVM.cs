namespace LiquorStoreFinalProject.Areas.Admin.ViewModels.Product
{
    public class UpdateProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int DiscountId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string? ImageURL { get; set; }
        public IFormFile? Image { get; set; }
    }
}
