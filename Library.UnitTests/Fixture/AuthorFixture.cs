using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.UnitTests.Fixture
{
    public static class AuthorFixture
    {
        public static List<Author> GetAuthorListFixture()
        {
            return new List<Author>()
            {
                new Author
                {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Name = "Test",
                    LastName= "Test",
                    BirthDate= DateTime.Now,
                    Email= "Test@gmail.com",
                    Gender = Domain.Enums.Gender.Female
                },
                new Author
                {
                    Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    Name = "Test2",
                    LastName= "Test2",
                    BirthDate= DateTime.Now,
                    Email= "Test2@gmail.com",
                    Gender = Domain.Enums.Gender.Male
                },
            };
        }

        public static Author GetAuthorFixture()
        {
            return GetAuthorListFixture().First();
        }
    }
}