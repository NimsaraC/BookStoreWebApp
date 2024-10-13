using BookStoreWebApp.DTOs;
using BookStoreWebApp.Models;
using BookStoreWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStoreWebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;
        private readonly CartService _cartService;
        private readonly IWebHostEnvironment _environment;

        public BookController(BookService bookService, CartService cartService, IWebHostEnvironment environment)
        {
            _bookService = bookService;
            _cartService = cartService;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }
        public async Task<IActionResult> AdminBook()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }
        [HttpGet]
        public IActionResult CreateNew()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateNew(BookCreateDtoView bookCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(bookCreateDto);
            }

            string? imagePath = null;

            if (bookCreateDto.ImageFile != null && bookCreateDto.ImageFile.Length > 0)
            {
                // Validate the image (e.g., size, type)
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(bookCreateDto.ImageFile.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    return BadRequest("Unsupported file type. Allowed types are .jpg, .jpeg, .png, .gif.");
                }

                // Optionally, limit the file size (e.g., 5MB)
                if (bookCreateDto.ImageFile.Length > 5 * 1024 * 1024)
                {
                    return BadRequest("File size exceeds the 5MB limit.");
                }

                // Generate a unique file name
                var fileName = Guid.NewGuid().ToString() + extension;
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await bookCreateDto.ImageFile.CopyToAsync(stream);
                }

                // Set the image path relative to wwwroot
                imagePath = $"/uploads/{fileName}";


            }
            var book = new BookCreateDto
            {
                Title = bookCreateDto.Title,
                Description = bookCreateDto.Description,
                Author = bookCreateDto.Author,
                Price = bookCreateDto.Price,
                Stock = bookCreateDto.Stock,
                ISBN = bookCreateDto.ISBN,
                Publisher = bookCreateDto.Publisher,
                PublicationDate = bookCreateDto.PublicationDate,
                ImagePath = imagePath
            };

            await _bookService.CreateBookAsync(book);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AddCart(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            var item = new AddCartItemDto
            {
                BookId = book.Id,
                Quantity = 1,
                UnitPrice = book.Price,
                ImagePath =book.ImagePath,
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
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            var newBook = new BookCreateDto
            {
                Author = book.Author,
                Description = book.Description,
                ISBN = book.ISBN,
                Price = book.Price,
                PublicationDate = book.PublicationDate,
                Publisher = book.Publisher,
                Stock = book.Stock,
                Title = book.Title,
            };
            if (book == null)
            {
                return NotFound();
            }
            return View(newBook);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookCreateDto bookCreateDto)
        {

            if (ModelState.IsValid)
            {
                await _bookService.UpdateBookAsync(id, bookCreateDto);
                return RedirectToAction("Index", "Admin");
            }
            return View(bookCreateDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction("AdminBook", "Book");
        }


    }
}
