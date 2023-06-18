using Library.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.UnitTests.Persistence
{
    public static class TestContext
    {
        public static LibraryDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            return new LibraryDbContext(options);
        }
    }
}