namespace BookStoreApp.API.Models.Book
{
    public class BookDetailsDto : BaseDto
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Isbn { get; set; } = null!;
        public string Summary { get; set; }
        public string ImageURL { get; set; }
        public string OriginalImageName { get; set; }
        public decimal Price { get; set; }
        public string AuthorName { get; set; }
        public int? AuthorId { get; set; }

    }
}
