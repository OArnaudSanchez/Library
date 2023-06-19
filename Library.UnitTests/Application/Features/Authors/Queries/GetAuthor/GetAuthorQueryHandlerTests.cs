using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Features.Authors.Queries.GetAuthor;
using Library.Application.Interfaces.Persistence;
using Library.Application.Mappings;
using Library.UnitTests.Fixture;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTests.Application.Features.Authors.Queries.GetAuthor
{
    public class GetAuthorQueryHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;

        private readonly IMapper _mapper;

        private readonly GetAuthorQueryHandler _sut;

        public GetAuthorQueryHandlerTests()
        {
            _authorRepositoryMock = new();

            var configuration = new MapperConfiguration(config => config.AddProfile(new AuthorMappingConfiguration()));
            _mapper = new Mapper(configuration);
            _sut = new GetAuthorQueryHandler(_authorRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_Should_ReturnAnAuthorDto()
        {
            //Arrange
            var testAuthor = AuthorFixture.GetAuthorFixture();
            var getAuthorQuery = new GetAuthorQuery { AuthorId = testAuthor.Id };
            _authorRepositoryMock.Setup(x => x.GetAuthorAsync(testAuthor.Id, It.IsAny<CancellationToken>())).ReturnsAsync(testAuthor);

            //Act
            var result = await _sut.Handle(getAuthorQuery, It.IsAny<CancellationToken>());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(testAuthor.Id, result.AuthorId);
            Assert.IsType<AuthorDto>(result);
        }
    }
}