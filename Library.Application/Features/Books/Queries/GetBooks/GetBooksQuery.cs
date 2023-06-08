using Library.Application.DTOs;
using MediatR;

namespace Library.Application.Features.Books.Queries.GetBooks
{
    public class GetBooksQuery : IRequest<IEnumerable<BookDto>>
    {
    }
}