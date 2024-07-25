using Institutional.Application.Common;
using FluentValidation;

namespace Institutional.Application.Features.Volunteers.EnrollVolunteer;

public class EnrollVolunteerValidator : AbstractValidator<EnrollVolunteerRequest>
{
    public EnrollVolunteerValidator()
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