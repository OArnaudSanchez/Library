using Library.Application.DTOs;
using MediatR;

namespace Library.Application.Features.Authors.Queries.GetAuthor
{
    public class GetAuthorQuery : IRequest<AuthorDto>
    {
        public Guid AuthorId { get; set; }
    }
}