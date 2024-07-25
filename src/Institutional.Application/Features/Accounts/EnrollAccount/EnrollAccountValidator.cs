using Institutional.Application.Common;
using FluentValidation;

namespace Institutional.Application.Features.Accounts.EnrollAccount;

public class EnrollAccountValidator : AbstractValidator<EnrollAccountRequest>
{
    public EnrollAccountValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);
        
        RuleFor(x => x.Nickname)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);
        
        RuleFor(x => x.BirthDate)
            .NotEmpty();
    }
}