using LiquorStoreFinalProject.Areas.Admin.ViewModels.Discount;
using LiquorStoreFinalProject.Helpers;
using LiquorStoreFinalProject.Models;
using LiquorStoreFinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiquorStoreFinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var discounts = _discountService.GetAll();
            return View(discounts);
        }
        [HttpGet]
        public IActionResult CreateDiscount()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateDiscountVM createDiscountVM)
        {
            await _discountService.CreateAsync(createDiscountVM);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDiscount(int id)
        {

            Discount discount = _discountService.GetById(id);
            UpdateDiscountVM updateDicountVM = new UpdateDiscountVM()
            {
               Id=discount.Id,
               Name=discount.Name,
               Percent=discount.Percent
            };
            return View(updateDicountVM);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDiscount(UpdateDiscountVM request)
        {
            await _discountService.UpdateAsync(request);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            await _discountService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
