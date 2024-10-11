using BookStoreWebApp.DTOs;

namespace BookStoreWebApp.Models
{
    public class AdminModel
    {
        public IEnumerable<BookDto> Books { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}
