using Library.Application.DTOs;
using Library.Application.Features.Authors.Queries.GetAuthor;
using Library.Application.Features.Authors.Queries.GetAuthors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = nameof(GetAuthors))]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var authors = await _mediator.Send(new GetAuthorsQuery());
            return Ok(authors);
        }

        [HttpGet("{authorId}", Name = nameof(GetAuthor))]
        public async Task<ActionResult<AuthorDto>> GetAuthor(Guid authorId)
        {
            var author = await _mediator.Send(new GetAuthorQuery { AuthorId = authorId });
            return Ok(author);
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