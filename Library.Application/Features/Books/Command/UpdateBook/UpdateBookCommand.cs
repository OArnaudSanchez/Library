using MediatR;

namespace Library.Application.Features.Books.Command.UpdateBook
{
    public class UpdateBookCommand : IRequest
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Ibsn { get; set; }
    }
}