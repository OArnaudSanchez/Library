using FluentValidation;

namespace Library.Application.Features.Books.Command.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.BookId)
                .NotNull()
                .NotEmpty();
        }
    }
}