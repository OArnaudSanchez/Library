using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.UnitTests.Fixture
{
    public static class BookFixture
    {
        public static List<Book> GetBookListFixture()
        {
            return new List<Book>
            {
                new Book
                {
                    Id = new Guid("fcfb0e6b-80ac-4581-a4a7-9972619385ee"),
                    Title = "Test Title 1",
                    Description = "Test Description 1",
                    ReleaseDate = DateTime.Now,
                    Ibsn = "ABCD1",
                    AuthorId = AuthorFixture.GetAuthorFixture().Id
                },
                new Book
                {
                    Id = new Guid("cca1cd5a-c7eb-4302-b6e7-24aa9f1c20bc"),
                    Title = "Test Title 2",
                    Description = "Test Description 2",
                    ReleaseDate = DateTime.Now,
                    Ibsn = "ABCD2",
                    AuthorId = AuthorFixture.GetAuthorFixture() .Id
                },
            };
        }

        public static Book GetBookFixture()
        {
            return GetBookListFixture().First();
        }
    }
}