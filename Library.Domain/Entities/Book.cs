using Library.Domain.Common;

namespace Library.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Ibsn { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Guid AuthorId { get; set; }
    }
}