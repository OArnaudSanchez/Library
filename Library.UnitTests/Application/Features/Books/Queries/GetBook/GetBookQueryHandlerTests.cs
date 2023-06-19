using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Features.Books.Queries.GetBook;
using Library.Application.Interfaces.Persistence;
using Library.Application.Mappings;
using Library.UnitTests.Fixture;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTests.Application.Features.Books.Queries.GetBook
{
    public class GetBookQueryHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;

        private readonly IMapper _mapper;

        private readonly GetBookQueryHandler _sut;

        public GetBookQueryHandlerTests()
        {
            var configuration = new MapperConfiguration(config => config.AddProfile(new BookMappingConfiguration()));
            _mapper = new Mapper(configuration);

            _bookRepositoryMock = new Mock<IBookRepository>();
            _sut = new GetBookQueryHandler(_mapper, _bookRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnABookDto()
        {
            //Arrange
            var testBook = BookFixture.GetBookFixture();
            var getBookQuery = new GetBookQuery { BookId = testBook.Id };
            _bookRepositoryMock.Setup(x => x.GetBookAsync(testBook.Id, It.IsAny<CancellationToken>())).ReturnsAsync(testBook);

            //Act
            var result = await _sut.Handle(getBookQuery, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(testBook.Id, result.BookId);
            Assert.IsType<BookDto>(result);
        }
    }
}