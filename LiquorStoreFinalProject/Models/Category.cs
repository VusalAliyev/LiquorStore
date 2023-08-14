namespace LiquorStoreFinalProject.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
