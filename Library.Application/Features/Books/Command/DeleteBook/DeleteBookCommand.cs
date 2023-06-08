using MediatR;

namespace Library.Application.Features.Books.Command.DeleteBook
{
    public class DeleteBookCommand : IRequest
    {
        public Guid BookId { get; set; }
    }
}