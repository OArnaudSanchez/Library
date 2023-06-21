using Library.Application.DTOs;
using Library.Application.Features.Books.Command.CreateBook;
using Library.Application.Features.Books.Command.DeleteBook;
using Library.Application.Features.Books.Command.UpdateBook;
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
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost(Name = nameof(CreateBook))]
        public async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookCommand bookCommand)
        {
            var createdBook = await _mediator.Send(bookCommand);
            return Created(nameof(GetBook), new { bookId = createdBook.BookId, createdBook });
        }

        [HttpPut(Name = nameof(UpdateBook))]
        public async Task<ActionResult> UpdateBook([FromBody] UpdateBookCommand bookCommand)
        {
            await _mediator.Send(bookCommand);
            return NoContent();
        }

        [HttpDelete("bookId", Name = nameof(DeleteBook))]
        public async Task<ActionResult> DeleteBook(Guid bookId)
        {
            await _mediator.Send(new DeleteBookCommand { BookId = bookId });
            return NoContent();
        }
    }
}