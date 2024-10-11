using BookStoreWebApp.DTOs;
using BookStoreWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<IActionResult> Index()
        {
            int id = 1;
            var items = await _cartService.GetCartItemsAsync(id);
            return View(items);
        }
        public async Task<IActionResult> UpdateQuantity(CartItemDto editItem)
        {
            if (ModelState.IsValid)
            {
                int id = editItem.Id;
                int item = editItem.Quantity;
                await _cartService.UpdateQuantiytAsync(item, id);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _cartService.DeleteOrderAsync(id);
            return RedirectToAction("Index");

        }
    }
}
