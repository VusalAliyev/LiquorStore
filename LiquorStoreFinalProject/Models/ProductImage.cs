namespace LiquorStoreFinalProject.Models
{
    public class ProductImage : BaseEntity
    {
        public string ImageURL { get; set; }
        public Product Product { get; set; }
    }
}
