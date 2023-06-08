using Library.Application.DTOs;
using MediatR;

namespace Library.Application.Features.Books.Queries.GetBook
{
    public class GetBookQuery : IRequest<BookDto>
    {
        public Guid BookId { get; set; }
    }
}