using BookStoreWebApp.Models;
using BookStoreWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly BookService _bookService;
        private readonly UserService _userService;
        private readonly OrderService _orderService;

        public AdminController(BookService bookService, UserService userService, OrderService orderService)
        {
            _bookService = bookService;
            _userService = userService;
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            var orders = await _orderService.GetAllOrdersAsync();
            var users = await _userService.GetAllUsersAsync();

            var data = new AdminModel
            {
                Books = books,
                Orders = orders,
                Users = users
            };

            return View(data);
        }
    }
}
