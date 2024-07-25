using Institutional.Application.Common;
using FluentValidation;

namespace Institutional.Application.Features.Accounts.Volunteer;

public class VolunteerValidator : AbstractValidator<VolunteerRequest>
{
    public VolunteerValidator()
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