using Library.Domain.Common;
using Library.Domain.Enums;

namespace Library.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }
    }
}