using FluentValidation;

namespace Institutional.Application.Features.Accounts.Photo;

public class PhotoValidator : AbstractValidator<PhotoRequest>
{
    public PhotoValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        RuleFor(x => x.FileExtension)
            .NotEmpty()
            .Must(x => x.Equals(".jpg") || x.Equals(".jpeg") || x.Equals(".png"));
    }
}