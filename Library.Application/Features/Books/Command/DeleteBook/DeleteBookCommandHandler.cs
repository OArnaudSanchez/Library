using Library.Application.Exceptions;
using Library.Application.Interfaces.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Books.Command.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _bookRepository;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var currentBook = await _bookRepository.GetBookAsync(request.BookId, cancellationToken);
            if (currentBook == null)
            {
                throw new NotFoundException(nameof(Book), request.BookId);
            }
            _bookRepository.DeleteBookSync(currentBook);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}