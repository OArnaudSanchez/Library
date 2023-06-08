using Library.Application.Interfaces.Persistence;
using MediatR;

namespace Library.Application.Features.Authors.Command.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var currentAuthor = await _authorRepository.GetAuthorAsync(request.AuthorId, cancellationToken);
            _authorRepository.DeleteAuthorSync(currentAuthor);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}