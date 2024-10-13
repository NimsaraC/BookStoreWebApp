using System.ComponentModel.DataAnnotations;

namespace BookStoreWebApp.DTOs
{
    public class BookDto
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
    public class BookCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        [Display(Name = "Book Cover Image")]
        public string? ImagePath { get; set; }
    }
    public class BookCreateDtoView
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        [Display(Name = "Book Cover Image")]
        public IFormFile ImageFile { get; set; }
    }
}
