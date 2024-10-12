using BookStoreWebApp.DTOs;
using BookStoreWebApp.Models;
using BookStoreWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly BookService _bookService;

        public OrderController(OrderService orderService, BookService bookService)
        {
            _orderService = orderService;
            _bookService = bookService;
        }
        public async Task<IActionResult> Index()
        {
            int id = 1;
            var books = await _bookService.GetAllBooksAsync();
            var items = await _orderService.GetAllOrdersByUserAsync(id);
            var data = new UserOrder{
                Books = books,
                Orders = items
            };
            return View(data);
        }
        public async Task<IActionResult> AdminOrders()
        {
            var items = await _orderService.GetAllOrdersAsync();
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
