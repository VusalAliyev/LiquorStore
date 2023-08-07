namespace LiquorStoreFinalProject.ViewModels
{
    public class GetPaginatedBlogVM
    {
        public List<GetAllBlogVM> Blogs { get; set; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
    }
}
