using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Features.Books.Command.CreateBook;
using Library.Application.Interfaces.Persistence;
using Library.Application.Mappings;
using Library.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTests.Application.Features.Books.Command.CreateBook
{
    public class CreateBookCommandHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;

        private readonly Mock<IBookRepository> _bookRepositoryMock;

        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly IMapper _mapper;

        private readonly CreateBookCommandHandler _sut;

        public CreateBookCommandHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _bookRepositoryMock = new Mock<IBookRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            var configuration = new MapperConfiguration(config => config.AddProfile(new BookMappingConfiguration()));
            _mapper = new Mapper(configuration);

            _sut = new CreateBookCommandHandler(
                _authorRepositoryMock.Object,
                _bookRepositoryMock.Object,
                _mapper, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handler_Should_CreateAnbookAndReturnbookDto()
        {
            //Arrange
            _authorRepositoryMock.Setup(x => x.GetAuthorAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Author());

            var request = new CreateBookCommand
            {
                Title = "Test",
                Description = "Test",
                ReleaseDate = DateTime.UtcNow,
                Ibsn = "Test",
                AuthorId = Guid.NewGuid()
            };

            //Act
            var result = await _sut.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            _bookRepositoryMock.Verify(x => x.AddBookAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<BookDto>(result);
        }
    }
}