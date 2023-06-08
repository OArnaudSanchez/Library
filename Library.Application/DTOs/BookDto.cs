namespace Library.Application.DTOs
{
    public class BookDto
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Ibsn { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Guid AuthorId { get; set; }
    }
}