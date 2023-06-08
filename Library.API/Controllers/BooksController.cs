using Library.Application.DTOs;
using Library.Application.Features.Books.Queries.GetBook;
using Library.Application.Features.Books.Queries.GetBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = nameof(GetBooks))]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _mediator.Send(new GetBooksQuery());
            return Ok(books);
        }

        [HttpGet("{bookId}", Name = nameof(GetBook))]
        public async Task<ActionResult<BookDto>> GetBook(Guid bookId)
        {
            var book = await _mediator.Send(new GetBookQuery { BookId = bookId });
            return Ok(book);
        }

        //TODO:
        /*
            1. Create Missing Controllers,
            2. Create Validations with FluentValidation,
            3. Add ILogger,
            4. Add GlobalExceptionFilters and Exceptions,
            5. Add Tests
            6. Add Log Properties
            7. Add Content Negociation
            8. Add Hateoas
            9. Add CI Pipeline
         */
    }
}