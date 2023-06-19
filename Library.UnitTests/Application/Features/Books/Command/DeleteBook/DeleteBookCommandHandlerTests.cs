using Library.Application.Exceptions;
using Library.Application.Features.Books.Command.DeleteBook;
using Library.Application.Interfaces.Persistence;
using Library.Domain.Entities;
using Library.UnitTests.Fixture;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTests.Application.Features.Books.Command.DeleteBook
{
    public class DeleteBookCommandHandlerTests
    {
        private readonly DeleteBookCommandHandler _sut;

        private readonly Mock<IBookRepository> _bookRepositoryMock;

        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteBookCommandHandlerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _sut = new DeleteBookCommandHandler(_bookRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handler_Shlould_DeleteABook()
        {
            //Arrange
            var testBook = BookFixture.GetBookFixture();
            _bookRepositoryMock.Setup(x => x.GetBookAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(testBook);

            //Act
            await _sut.Handle(new DeleteBookCommand(), It.IsAny<CancellationToken>());

            //Assert
            _bookRepositoryMock.Verify(x => x.DeleteBookSync(It.IsAny<Book>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_ThrowAnExceptionIfAuthorIsNotFound()
        {
            //Arrange
            _bookRepositoryMock.Setup(x => x.GetBookAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(value: null);

            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(new DeleteBookCommand(), It.IsAny<CancellationToken>()));
            _bookRepositoryMock.Verify(x => x.UpdateBookSync(It.IsAny<Book>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}