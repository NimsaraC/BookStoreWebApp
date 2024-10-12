using BookStoreWebApp.DTOs;

namespace BookStoreWebApp.Models
{
    public class UserProfile
    {
        public IEnumerable<OrderDto> Orders { get; set; }
        public UserDto User { get; set; }
    }
}
