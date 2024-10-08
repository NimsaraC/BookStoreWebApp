using BookStoreWebApp.DTOs;
using BookStoreWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            int id = 1;
            var items = await _orderService.GetAllOrdersByUserAsync(id);
            return View(items);
        }
        [HttpGet]
        public IActionResult MakeOrder()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MakeOrder(CreateOrderDto createOrderDto)
        {
            if (ModelState.IsValid)
            {
                await _orderService.CreateOrderAsync(createOrderDto);
                return RedirectToAction("Index");
            }
            return View(createOrderDto);
        }
    }
}
