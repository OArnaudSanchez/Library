using Library.Application.DTOs;
using MediatR;

namespace Library.Application.Features.Books.Command.CreateBook
{
    public class CreateBookCommand : IRequest<BookDto>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Ibsn { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Guid AuthorId { get; set; }
    }
}