using Library.Application.Exceptions;
using Library.Application.Interfaces.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Books.Command.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IBookRepository _bookRepository;

        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var currentBook = await _bookRepository.GetBookAsync(request.BookId, cancellationToken);
            if (currentBook == null)
            {
                throw new NotFoundException(nameof(Book), request.BookId);
            }
            currentBook.Title = request.Title ?? currentBook.Title;
            currentBook.Description = request.Description ?? currentBook.Description;
            currentBook.Ibsn = request.Ibsn ?? currentBook.Ibsn;
            currentBook.Id = currentBook.Id;
            currentBook.ReleaseDate = currentBook.ReleaseDate;
            currentBook.AuthorId = currentBook.AuthorId;

            _bookRepository.UpdateBookSync(currentBook);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}