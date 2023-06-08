using Library.Application.DTOs;
using Library.Domain.Enums;
using MediatR;

namespace Library.Application.Features.Authors.Command.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<AuthorDto>
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }
    }
}