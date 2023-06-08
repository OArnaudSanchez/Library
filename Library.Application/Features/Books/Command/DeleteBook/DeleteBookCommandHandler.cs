using Library.Application.Interfaces.Persistence;
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
            var currentBoook = await _bookRepository.GetBookAsync(request.BookId, cancellationToken);
            _bookRepository.DeleteBookSync(currentBoook);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}