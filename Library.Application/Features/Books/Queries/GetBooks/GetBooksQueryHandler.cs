using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Interfaces.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Books.Queries.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
    {
        private readonly IMapper _mapper;

        private readonly IBookRepository _bookRepository;

        public GetBooksQueryHandler(IMapper mapper, IBookRepository bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetBooksAsync(cancellationToken);
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(books);
        }
    }
}