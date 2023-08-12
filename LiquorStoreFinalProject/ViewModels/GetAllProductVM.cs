using LiquorStoreFinalProject.Models;

namespace LiquorStoreFinalProject.ViewModels
{
    public class GetAllProductVM
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public string DiscountName { get; set; }
        public int DiscountId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
