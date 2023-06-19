using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Features.Books.Queries.GetBooks;
using Library.Application.Interfaces.Persistence;
using Library.Application.Mappings;
using Library.Domain.Entities;
using Library.UnitTests.Fixture;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTests.Application.Features.Books.Queries.GetBooks
{
    public class GetBooksQueryHandlerTests
    {
        private readonly IMapper _mapper;

        private readonly Mock<IBookRepository> _bookRepositoryMock;

        private readonly List<Book> _books;

        public GetBooksQueryHandlerTests()
        {
            _books = BookFixture.GetBookListFixture();
            _bookRepositoryMock = new Mock<IBookRepository>();
            _bookRepositoryMock.Setup(x => x.GetBooksAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_books);

            var configuration = new MapperConfiguration(config => config.AddProfile(new BookMappingConfiguration()));
            _mapper = new Mapper(configuration);
        }

        [Fact]
        public async Task Handle_Should_ReturnListOfBooksDto()
        {
            //Arrange
            GetBooksQueryHandler sut = new GetBooksQueryHandler(_mapper, _bookRepositoryMock.Object);
            var query = new GetBooksQuery();

            //Act
            var result = await sut.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            Assert.Equal(_books.Count, result.Count());
            Assert.IsType<List<BookDto>>(result);
        }
    }
}