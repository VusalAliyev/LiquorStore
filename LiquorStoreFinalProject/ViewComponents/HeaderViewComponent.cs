using LiquorStoreFinalProject.Data;
using LiquorStoreFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LiquorStoreFinalProject.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        public HeaderViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            //List<BasketVM> basketList = new();

            //if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            //{
            //    List<BasketVM> basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);

            //    foreach (var item in basketDatas)
            //    {
            //        var dbProduct = _context.Products.FirstOrDefault(p => p.Id == item.Id);

            //        if (dbProduct != null)
            //        {
            //            BasketVM basketDetail = new()
            //            {
            //                Id = dbProduct.Id,
            //                Name = dbProduct.Name,
            //                Image = dbProduct.ImageURL,
            //                Count = item.Count,
            //                Price = dbProduct.Price,
            //                TotalPrice = item.Count * dbProduct.Price
            //            };

            //            basketList.Add(basketDetail);
            //        }

            //    }
            //}

            return View();
        }
    }
}
