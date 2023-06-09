﻿using Library.Application.Exceptions;
using Library.Application.Interfaces.Persistence;
using Library.Domain.Entities;
using MediatR;

namespace Library.Application.Features.Authors.Command.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository;

        private readonly IUnitOfWork _unitOfWork;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var currentAuthor = await _authorRepository.GetAuthorAsync(request.AuthorId, cancellationToken);
            if (currentAuthor == null)
            {
                throw new NotFoundException(nameof(Author), request.AuthorId);
            }
            currentAuthor.Id = currentAuthor.Id;
            currentAuthor.Name = currentAuthor.Name;
            currentAuthor.LastName = currentAuthor.LastName;
            currentAuthor.Email = request.Email ?? currentAuthor.Email;
            currentAuthor.Gender = request.Gender;
            currentAuthor.BirthDate = currentAuthor.BirthDate;

            _authorRepository.UpdateAuthorSync(currentAuthor);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}