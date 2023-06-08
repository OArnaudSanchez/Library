using Library.Domain.Enums;

namespace Library.Application.DTOs
{
    public class AuthorDto
    {
        public Guid AuthorId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }
    }
}