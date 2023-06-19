using Library.Application.Exceptions;
using Library.Application.Features.Books.Command.UpdateBook;
using Library.Application.Interfaces.Persistence;
using Library.Domain.Entities;
using Library.UnitTests.Fixture;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTests.Application.Features.Books.Command.UpdateBook
{
    public class UpdateBookCommandHandlerTests
    {
        private readonly UpdateBookCommandHandler _sut;

        private readonly Mock<IBookRepository> _BookRepositoryMock;

        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateBookCommandHandlerTests()
        {
            _BookRepositoryMock = new Mock<IBookRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _sut = new UpdateBookCommandHandler(_BookRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handler_Should_UpdateAnBook()
        {
            //Arrange
            var testBook = BookFixture.GetBookFixture();
            _BookRepositoryMock.Setup(x => x.GetBookAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(testBook);
            var request = new UpdateBookCommand
            {
                BookId = testBook.Id,
                Title = "Test Title",
                Description = "Test Description",
                Ibsn = "Test IBSN"
            };

            //Act
            await _sut.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            _BookRepositoryMock.Verify(x => x.UpdateBookSync(It.IsAny<Book>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_ThrowAnExceptionIfBookIsNotFound()
        {
            //Arrange
            _BookRepositoryMock.Setup(x => x.GetBookAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(value: null);

            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(new UpdateBookCommand(), It.IsAny<CancellationToken>()));
            _BookRepositoryMock.Verify(x => x.UpdateBookSync(It.IsAny<Book>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}