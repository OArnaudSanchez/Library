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
    public class AuthorRepositoryTests
    {
        private readonly List<Author> _authors;

        private readonly AuthorRepository _sut;

        private readonly LibraryDbContext _dbContext;

        private readonly IUnitOfWork _unitOfWork;

        public AuthorRepositoryTests()
        {
            _authors = AuthorFixture.GetAuthorListFixture();
            _dbContext = TestContext.GetDbContext();
            _dbContext.Authors.AddRange(_authors);
            _dbContext.SaveChanges();
            _sut = new AuthorRepository(_dbContext);
            _unitOfWork = new UnitOfWork(_dbContext);
            _dbContext.ChangeTracker.Clear();
        }

        [Fact]
        public async Task AuthorRepository_Should_Return_AllAuthors()
        {
            //Act
            var result = await _sut.GetAuthorsAsync(It.IsAny<CancellationToken>());

            //Assert
            Assert.True(_authors.Count == result.Count());
        }

        [Fact]
        public async Task AuthorRepository_Should_Return_AnAuthorByGuid()
        {
            //Arrange
            var testAuthorGuid = AuthorFixture.GetAuthorFixture().Id;

            //Act
            var result = await _sut.GetAuthorAsync(testAuthorGuid, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
        }

        [Fact]
        public async Task AuthorRepository_Sould_AddNewAuthor()
        {
            //Arrange
            var newAuthor = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Test3",
                LastName = "Test3",
                BirthDate = DateTime.Now,
                Email = "Test3@gmail.com",
                Gender = Domain.Enums.Gender.Male
            };

            //Act
            await _sut.AddAuthorAsync(newAuthor, It.IsAny<CancellationToken>());
            var result = await _unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>());
            var allAuthorsDb = await _sut.GetAuthorsAsync(It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(1, result);
            Assert.Equal(_authors.Count + 1, allAuthorsDb.Count());
            Assert.Contains(newAuthor, allAuthorsDb);
        }

        [Fact]
        public async Task AuthorRepository_Should_UpdateAuthor()
        {
            //Arrange
            var testAuthorGuid = AuthorFixture.GetAuthorFixture().Id;
            var updateAuthor = new Author
            {
                Id = testAuthorGuid,
                Name = "Updated Author",
                LastName = "Test3",
                BirthDate = DateTime.Now,
                Email = "updated@gmail.com",
                Gender = Domain.Enums.Gender.Female
            };

            //Act
            _sut.UpdateAuthorSync(updateAuthor);
            var result = await _unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>());
            var currentAuthor = await _sut.GetAuthorAsync(testAuthorGuid, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(1, result);
            Assert.Same(updateAuthor, currentAuthor);
            Assert.Contains("updated", currentAuthor.Name, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task AuthorRepository_Sould_DeleteAnAuthor()
        {
            //Arrange
            var testAuthorGuid = AuthorFixture.GetAuthorFixture().Id;
            var currentAuthor = await _sut.GetAuthorAsync(testAuthorGuid, It.IsAny<CancellationToken>());

            //Act
            _sut.DeleteAuthorSync(currentAuthor);
            var result = await _unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>());
            var allAuthors = await _sut.GetAuthorsAsync(It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(1, result);
            Assert.Equal(_authors.Count - 1, allAuthors.Count());
            Assert.NotSame(_authors, allAuthors);
        }
    }
}