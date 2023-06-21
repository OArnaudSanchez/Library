using AutoMapper;
using Library.API.Controllers;
using Library.Application.DTOs;
using Library.Application.Features.Authors.Command.CreateAuthor;
using Library.Application.Features.Authors.Command.UpdateAuthor;
using Library.Application.Features.Authors.Queries.GetAuthor;
using Library.Application.Features.Authors.Queries.GetAuthors;
using Library.Application.Mappings;
using Library.Domain.Entities;
using Library.UnitTests.Fixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTests.API.Controllers
{
    public class AuthorsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;

        private readonly AuthorsController _sut;

        private readonly IMapper _mapper;

        public AuthorsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _sut = new AuthorsController(_mediatorMock.Object);
            var configuration = new MapperConfiguration(config => config.AddProfile(new AuthorMappingConfiguration()));
            _mapper = new Mapper(configuration);
        }

        [Fact]
        public async Task GetAuthors_Should_ReturnAllAuthorsAnd200Ok()
        {
            //Arrange
            var authors = AuthorFixture.GetAuthorListFixture();
            var authorsDto = _mapper.Map<List<Author>, IEnumerable<AuthorDto>>(authors);
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetAuthorsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(authorsDto);

            //Act
            var response = await _sut.GetAuthors();
            var result = response.Result as OkObjectResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(authorsDto, result?.Value);
            Assert.Equal(200, result?.StatusCode);
        }

        [Fact]
        public async Task GetAuthor_Should_ReturnAnAuthorAnd200Ok()
        {
            //Arrange
            var author = AuthorFixture.GetAuthorFixture();
            var authorDto = _mapper.Map<Author, AuthorDto>(author);
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetAuthorQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(authorDto);

            //Act
            var response = await _sut.GetAuthor(author.Id);
            var result = response.Result as OkObjectResult;

            //Assert
            var authorResult = Assert.IsType<AuthorDto>(result?.Value);
            Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(authorResult.AuthorId, author.Id);
        }

        [Fact]
        public async Task GetAuthor_Sould_Return404NotFound()
        {
            //Arrange
            var testAuthorId = Guid.NewGuid();

            //Act
            var response = await _sut.GetAuthor(testAuthorId);

            //Assert
            var result = Assert.IsType<NotFoundResult>(response.Result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task CreateAuthor_Should_CreateAuthorAndReturn201Created()
        {
            //Arrange
            var request = new CreateAuthorCommand
            {
                Name = "test",
                LastName = "test",
                BirthDate = DateTime.Now,
                Email = "test@gmail.com",
                Gender = Domain.Enums.Gender.Male
            };
            var validator = new CreateAuthorCommandValidator();

            _mediatorMock.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).ReturnsAsync(new AuthorDto());

            //Act
            var response = await _sut.CreateAuthor(request);
            var validationResult = await validator.ValidateAsync(request);

            //Assert
            Assert.Empty(validationResult.Errors);
            Assert.True(validationResult.IsValid);
            Assert.IsType<CreatedResult>(response.Result);
        }

        [Fact]
        public async Task CreateAuthor_Should_Return400BadRequestWhenRequestIsInvalid()
        {
            //Arrange
            var request = new CreateAuthorCommand();
            var validator = new CreateAuthorCommandValidator();
            _mediatorMock.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).ReturnsAsync(new AuthorDto());

            //Act
            await _sut.CreateAuthor(request);
            var validationResult = await validator.ValidateAsync(request);

            //Assert
            Assert.NotEmpty(validationResult.Errors);
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task UpdateAuthor_Should_Return204NoContentWhenUpdateSucceded()
        {
            //Arrange
            var request = new UpdateAuthorCommand
            {
                AuthorId = Guid.NewGuid(),
                Email = "Test@gmail.com",
                Gender = Domain.Enums.Gender.Female
            };
            var validator = new UpdateAuthorCommandValidator();

            //Act
            var response = await _sut.UpdateAuthor(request);
            var validationResult = await validator.ValidateAsync(request);

            //Assert
            Assert.True(validationResult.IsValid);
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async Task UpdateAuthor_Should_Return400BadRequestWhenRequestIsInvalid()
        {
            //Arrange
            var request = new UpdateAuthorCommand();
            var validator = new UpdateAuthorCommandValidator();

            //Act
            await _sut.UpdateAuthor(request);
            var validationResult = await validator.ValidateAsync(request);

            //Assert
            Assert.NotEmpty(validationResult.Errors);
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task DeleteAuthor_ShouldReturn204NoContentWhenDeleteSucceded()
        {
            //Arrange
            var request = Guid.NewGuid();

            //Act
            var response = await _sut.DeleteAuthor(request);

            //Assert
            Assert.IsType<NoContentResult>(response);
        }
    }
}