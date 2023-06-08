using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Interfaces.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Authors.Queries.GetAuthor
{
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, AuthorDto>
    {
        private readonly IAuthorRepository _authorRepository;

        private readonly IMapper _mapper;

        public GetAuthorQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<AuthorDto> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAuthorAsync(request.AuthorId, cancellationToken);
            return _mapper.Map<Author, AuthorDto>(author);
        }
    }
}