using BookStoreWebApp.DTOs;
using BookStoreWebApp.Models;
using BookStoreWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;
        private readonly CartService _cartService;

        public BookController(BookService bookService, CartService cartService)
        {
            _bookService = bookService;
            _cartService = cartService;

        }
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookCreateDto bookCreateDto)
        {
            if (ModelState.IsValid)
            {
                await _bookService.CreateBookAsync(bookCreateDto);
                return RedirectToAction("Index");
            }
            return View(bookCreateDto);
        }
        public async Task<IActionResult> AddCart(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            var item = new AddCartItemDto
            {
                BookId = book.Id,
                Quantity = 1,
                UnitPrice = book.Price
            };
            int userid = 1;
            await _cartService.AddCartAsync(item, userid);
            ViewData["QuoteSuccess"] = "Send successful!";
            ViewData["RedirectUrl"] = Url.Action("Index", "Supplier");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            /*if (book == null)
            {
                return NotFound();
            }*/
            return View(book);
        }


    }
}
