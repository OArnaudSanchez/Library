using Library.Application.Exceptions;
using Library.Application.Features.Authors.Command.DeleteAuthor;
using Library.Application.Interfaces.Persistence;
using Library.Domain.Entities;
using Library.UnitTests.Fixture;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTests.Application.Features.Authors.Command.DeleteAuthor
{
    public class DeleteAuthorCommandHandlerTests
    {
        private readonly DeleteAuthorCommandHandler _sut;

        private readonly Mock<IAuthorRepository> _authorRepositoryMock;

        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteAuthorCommandHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _sut = new DeleteAuthorCommandHandler(_authorRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handler_Should_DeleteAnAuthor()
        {
            //Arrange
            var testAuthor = AuthorFixture.GetAuthorFixture();
            _authorRepositoryMock.Setup(x => x.GetAuthorAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(testAuthor);

            //Act
            await _sut.Handle(new DeleteAuthorCommand(), It.IsAny<CancellationToken>());

            //Assert
            _authorRepositoryMock.Verify(x => x.DeleteAuthorSync(It.IsAny<Author>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_ThrowAnExceptionIfAuthorIsNotFound()
        {
            //Arrange
            _authorRepositoryMock.Setup(x => x.GetAuthorAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(value: null);

            //Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(new DeleteAuthorCommand(), It.IsAny<CancellationToken>()));
            _authorRepositoryMock.Verify(x => x.UpdateAuthorSync(It.IsAny<Author>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}