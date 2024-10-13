namespace BookStoreWebApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? ImagePath { get; set; }
    }
}
