using Library.Domain.Enums;
using MediatR;

namespace Library.Application.Features.Authors.Command.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest
    {
        public Guid AuthorId { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }
    }
}