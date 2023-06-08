using Library.Application.DTOs;
using MediatR;

namespace Library.Application.Features.Authors.Queries.GetAuthors
{
    public class GetAuthorsQuery : IRequest<IEnumerable<AuthorDto>>
    {
    }
}