namespace BookStoreWebApp.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateTime { get; set; }
        public double TotalAmount { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Status { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public string? ImagePath { get; set; }
    }

    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
    }
}
