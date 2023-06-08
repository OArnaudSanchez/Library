using MediatR;

namespace Library.Application.Features.Authors.Command.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest
    {
        public Guid AuthorId { get; set; }
    }
}