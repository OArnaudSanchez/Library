using AutoMapper;
using Library.API.Controllers;
using Library.Application.DTOs;
using Library.Application.Features.Books.Command.CreateBook;
using Library.Application.Features.Books.Command.UpdateBook;
using Library.Application.Features.Books.Queries.GetBook;
using Library.Application.Features.Books.Queries.GetBooks;
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
    public class BooksControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;

        private readonly BooksController _sut;

        private readonly IMapper _mapper;

        public BooksControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _sut = new BooksController(_mediatorMock.Object);
            var configuration = new MapperConfiguration(config => config.AddProfile(new BookMappingConfiguration()));
            _mapper = new Mapper(configuration);
        }

        [Fact]
        public async Task GetBooks_Should_ReturnAllBooksAnd200Ok()
        {
            //Arrange
            var Books = BookFixture.GetBookListFixture();
            var BooksDto = _mapper.Map<List<Book>, IEnumerable<BookDto>>(Books);
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetBooksQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(BooksDto);

            //Act
            var response = await _sut.GetBooks();
            var result = response.Result as OkObjectResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(BooksDto, result?.Value);
            Assert.Equal(200, result?.StatusCode);
        }

        [Fact]
        public async Task GetBook_Should_ReturnAnBookAnd200Ok()
        {
            //Arrange
            var Book = BookFixture.GetBookFixture();
            var BookDto = _mapper.Map<Book, BookDto>(Book);
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetBookQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(BookDto);

            //Act
            var response = await _sut.GetBook(Book.Id);
            var result = response.Result as OkObjectResult;

            //Assert
            var BookResult = Assert.IsType<BookDto>(result?.Value);
            Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(BookResult.BookId, Book.Id);
        }

        [Fact]
        public async Task GetBook_Sould_Return404NotFound()
        {
            //Arrange
            var testBookId = Guid.NewGuid();

            //Act
            var response = await _sut.GetBook(testBookId);

            //Assert
            var result = Assert.IsType<NotFoundResult>(response.Result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task CreateBook_Should_CreateBookAndReturn201Created()
        {
            //Arrange
            var request = new CreateBookCommand
            {
                Title = "Title",
                Description = "Description",
                ReleaseDate = DateTime.Now,
                Ibsn = "Test",
                AuthorId = Guid.NewGuid()
            };
            var validator = new CreateBookCommandValidator();

            _mediatorMock.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).ReturnsAsync(new BookDto());

            //Act
            var response = await _sut.CreateBook(request);
            var validationResult = await validator.ValidateAsync(request);

            //Assert
            Assert.Empty(validationResult.Errors);
            Assert.True(validationResult.IsValid);
            Assert.IsType<CreatedResult>(response.Result);
        }

        [Fact]
        public async Task CreateBook_Should_Return400BadRequestWhenRequestIsInvalid()
        {
            //Arrange
            var request = new CreateBookCommand();
            var validator = new CreateBookCommandValidator();
            _mediatorMock.Setup(x => x.Send(request, It.IsAny<CancellationToken>())).ReturnsAsync(new BookDto());

            //Act
            await _sut.CreateBook(request);
            var validationResult = await validator.ValidateAsync(request);

            //Assert
            Assert.NotEmpty(validationResult.Errors);
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task UpdateBook_Should_Return204NoContentWhenUpdateSucceded()
        {
            //Arrange
            var request = new UpdateBookCommand
            {
                BookId = Guid.NewGuid(),
                Description = "Test",
                Title = "Test",
                Ibsn = "Test"
            };
            var validator = new UpdateBookCommandValidator();

            //Act
            var response = await _sut.UpdateBook(request);
            var validationResult = await validator.ValidateAsync(request);

            //Assert
            Assert.True(validationResult.IsValid);
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async Task UpdateBook_Should_Return400BadRequestWhenRequestIsInvalid()
        {
            //Arrange
            var request = new UpdateBookCommand();
            var validator = new UpdateBookCommandValidator();

            //Act
            await _sut.UpdateBook(request);
            var validationResult = await validator.ValidateAsync(request);

            //Assert
            Assert.NotEmpty(validationResult.Errors);
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task DeleteBook_ShouldReturn204NoContentWhenDeleteSucceded()
        {
            //Arrange
            var request = Guid.NewGuid();

            //Act
            var response = await _sut.DeleteBook(request);

            //Assert
            Assert.IsType<NoContentResult>(response);
        }
    }
}