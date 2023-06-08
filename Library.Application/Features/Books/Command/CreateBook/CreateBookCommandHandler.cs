using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Interfaces.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Books.Command.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookDto>
    {
        private readonly IAuthorRepository _authorRepository;

        private readonly IBookRepository _bookRepository;

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public CreateBookCommandHandler(
            IAuthorRepository authorRepository,
            IBookRepository bookRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var currentAuthor = await _authorRepository.GetAuthorAsync(request.AuthorId, cancellationToken);
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                ReleaseDate = request.ReleaseDate,
                AuthorId = currentAuthor.Id,
                Ibsn = request.Ibsn
            };

            await _bookRepository.AddBookAsync(book, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<Book, BookDto>(book);
        }
    }
}