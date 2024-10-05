namespace BookStoreWebApp.DTOs
{
    public class CartDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItemDto> Items { get; set; }
    }

    public class CartItemDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }

    public class AddCartItemDto
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
