using LiquorStoreFinalProject.Controllers;
using LiquorStoreFinalProject.Models;

namespace LiquorStoreFinalProject.ViewModels
{
    public class GetPaginatedProductVM
    {
        public List<GetAllProductVM> Products { get; set; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
    }
}
