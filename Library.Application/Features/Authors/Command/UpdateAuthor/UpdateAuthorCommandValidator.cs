using FluentValidation;

namespace Library.Application.Features.Authors.Command.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(x => x.AuthorId)
                .NotNull()
                .NotEmpty();
        }
    }
}