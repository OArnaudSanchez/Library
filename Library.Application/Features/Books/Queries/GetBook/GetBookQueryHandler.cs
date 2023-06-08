using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Interfaces.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Books.Queries.GetBook
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookDto>
    {
        private readonly IMapper _mapper;

        private readonly IBookRepository _bookRepository;

        public GetBookQueryHandler(IMapper mapper, IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookAsync(request.BookId, cancellationToken);
            return _mapper.Map<Book, BookDto>(book);
        }
    }
}