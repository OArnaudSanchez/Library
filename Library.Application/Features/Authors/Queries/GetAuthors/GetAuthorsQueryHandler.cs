using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Interfaces.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Authors.Queries.GetAuthors
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<AuthorDto>>
    {
        private readonly IMapper _mapper;

        private readonly IAuthorRepository _authorRepository;

        public GetAuthorsQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _authorRepository.GetAuthorsAsync(cancellationToken);
            return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDto>>(authors);
        }
    }
}