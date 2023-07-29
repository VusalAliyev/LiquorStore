using LiquorStoreFinalProject.Models;

namespace LiquorStoreFinalProject.ViewModels
{
    public class GetAllProductVM
    {
        public int Id { get; set; }
        public int ProductImageId { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<Category> Categories { get; set; }
    }
}
