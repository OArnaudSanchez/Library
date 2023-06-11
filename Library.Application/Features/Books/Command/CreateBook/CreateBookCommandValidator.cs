using FluentValidation;

namespace Library.Application.Features.Books.Command.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.AuthorId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .MinimumLength(10);

            RuleFor(x => x.Ibsn)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.ReleaseDate)
                .NotNull()
                .LessThan(DateTime.Now.AddMinutes(1));
        }
    }
}