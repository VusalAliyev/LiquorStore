namespace LiquorStoreFinalProject.ViewModels
{
    public class BasketVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string DiscountName { get; set; }
        public int DiscountId { get; set; }
        public decimal Count { get; set; }
    }
}
