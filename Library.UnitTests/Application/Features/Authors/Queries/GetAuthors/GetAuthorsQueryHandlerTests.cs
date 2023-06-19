using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Features.Authors.Queries.GetAuthors;
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

namespace Library.UnitTests.Application.Features.Authors.Queries.GetAuthors
{
    public class GetAuthorsQueryHandlerTests
    {
        private readonly List<Author> _authors;

        private readonly Mock<IAuthorRepository> _authorRepositoryMock;

        private readonly IMapper _mapper;

        public GetAuthorsQueryHandlerTests()
        {
            _authors = AuthorFixture.GetAuthorListFixture();
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _authorRepositoryMock.Setup(x => x.GetAuthorsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_authors);

            var configuration = new MapperConfiguration(config => config.AddProfile(new AuthorMappingConfiguration()));
            _mapper = new Mapper(configuration);
        }

        [Fact]
        public async Task Handle_Should_ReturnListOfAuthorsDto()
        {
            //Arrange
            GetAuthorsQueryHandler sut = new GetAuthorsQueryHandler(_authorRepositoryMock.Object, _mapper);

            //Act
            var result = await sut.Handle(new GetAuthorsQuery(), It.IsAny<CancellationToken>());

            //Assert
            Assert.IsType<List<AuthorDto>>(result);
            Assert.Equal(_authors.Count, result.Count());
        }
    }
}