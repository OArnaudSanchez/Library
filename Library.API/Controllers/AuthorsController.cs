﻿using Library.Application.DTOs;
using Library.Application.Features.Authors.Command.CreateAuthor;
using Library.Application.Features.Authors.Command.DeleteAuthor;
using Library.Application.Features.Authors.Command.UpdateAuthor;
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
        //TODO: 1. Create Key Vault, Azure SQL Server DB, App Service and configure connection string from key vault, Create CI/CD Pipelines to deploy to Azure App Service.
        //TODO: 2. Document API, Centralize DbContext and DB Creation for Tests
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

        [HttpPost(Name = nameof(CreateAuthor))]
        public async Task<ActionResult<AuthorDto>> CreateAuthor([FromBody] CreateAuthorCommand authorCommand)
        {
            var createdAuthor = await _mediator.Send(authorCommand);
            return Created(nameof(GetAuthor), new { authorId = createdAuthor.AuthorId, createdAuthor });
        }

        [HttpPut(Name = nameof(UpdateAuthor))]
        public async Task<ActionResult> UpdateAuthor([FromBody] UpdateAuthorCommand authorCommand)
        {
            await _mediator.Send(authorCommand);
            return NoContent();
        }

        [HttpDelete("{authorId}", Name = nameof(DeleteAuthor))]
        public async Task<ActionResult> DeleteAuthor(Guid authorId)
        {
            await _mediator.Send(new DeleteAuthorCommand { AuthorId = authorId });
            return NoContent();
        }
    }
}