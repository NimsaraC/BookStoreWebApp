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
    }
}
