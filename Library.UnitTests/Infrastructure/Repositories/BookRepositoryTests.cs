using Library.Application.Interfaces.Persistence;
using Library.Domain.Entities;
using Library.Infrastructure.Persistence.Context;
using Library.Infrastructure.Repositories;
using Library.UnitTests.Fixture;
using Library.UnitTests.Persistence;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTests.Infrastructure.Repositories
{
    public class BookRepositoryTests
    {
        private readonly List<Book> _books;

        private readonly BookRepository _sut;

        private readonly LibraryDbContext _dbContext;

        private readonly IUnitOfWork _unitOfWork;

        public BookRepositoryTests()
        {
            _books = BookFixture.GetBookListFixture();
            _dbContext = TestContext.GetDbContext();
            _dbContext.Books.AddRange(_books);
            _dbContext.SaveChanges();
            _sut = new BookRepository(_dbContext);
            _unitOfWork = new UnitOfWork(_dbContext);
            _dbContext.ChangeTracker.Clear();
        }

        [Fact]
        public async Task BookRepository_Should_Return_AllBooks()
        {
            //Act
            var books = await _sut.GetBooksAsync(It.IsAny<CancellationToken>());

            //Assert
            Assert.True(_books.Count == books.Count());
        }

        [Fact]
        public async Task BookRepository_Should_Return_ABookByGuid()
        {
            //Arrange
            var testBookGuid = BookFixture.GetBookFixture().Id;

            //Act
            var book = await _sut.GetBookAsync(testBookGuid, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(book);
            Assert.IsType<Book>(book);
        }

        [Fact]
        public async Task BookRepository_Sould_AddNewBook()
        {
            //Arrange
            var newBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Test Title 12",
                Description = "Test Description 12",
                ReleaseDate = DateTime.Now,
                Ibsn = "ABCD12",
                AuthorId = AuthorFixture.GetAuthorFixture().Id
            };

            //Act
            await _sut.AddBookAsync(newBook, It.IsAny<CancellationToken>());
            var result = await _unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>());
            var allBooksDb = await _sut.GetBooksAsync(It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(1, result);
            Assert.Equal(_books.Count + 1, allBooksDb.Count());
            Assert.Contains(newBook, allBooksDb);
        }

        [Fact]
        public async Task BookRepository_Should_UpdateBook()
        {
            //Arrange
            var testBookGuid = BookFixture.GetBookFixture().Id;
            var currentBook = new Book
            {
                Id = testBookGuid,
                Title = "Updated Book",
                Description = "Test Description For an Updated Book",
                ReleaseDate = DateTime.Now,
                Ibsn = "ABCD12",
                AuthorId = AuthorFixture.GetAuthorFixture().Id
            };

            //Act
            _sut.UpdateBookSync(currentBook);
            var result = await _unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>());
            var updatedBook = await _sut.GetBookAsync(testBookGuid, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(1, result);
            Assert.Same(currentBook, updatedBook);
            Assert.Contains("updated", updatedBook.Title, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task BookRepository_Sould_DeleteABook()
        {
            //Arrange
            var testBookGuid = BookFixture.GetBookFixture().Id;
            var currentBook = await _sut.GetBookAsync(testBookGuid, It.IsAny<CancellationToken>());

            //Act
            _sut.DeleteBookSync(currentBook);
            var result = await _unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>());
            var allBooksDb = await _sut.GetBooksAsync(It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(1, result);
            Assert.Equal(_books.Count - 1, allBooksDb.Count());
            Assert.NotSame(_books, allBooksDb);
        }
    }
}