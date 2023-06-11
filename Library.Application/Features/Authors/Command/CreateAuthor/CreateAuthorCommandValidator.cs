using FluentValidation;

namespace Library.Application.Features.Authors.Command.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(x => x.Gender)
                .NotEmpty()
                .IsInEnum();

            RuleFor(x => x.BirthDate)
                .NotNull()
                .NotEmpty()
                .LessThan(DateTime.Now);
        }
    }
}