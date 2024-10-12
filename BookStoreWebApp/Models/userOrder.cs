using BookStoreWebApp.DTOs;

namespace BookStoreWebApp.Models
{
    public class UserOrder
    {
        public IEnumerable<BookDto> Books { get; set; }
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}
