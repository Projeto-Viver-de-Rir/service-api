using Institutional.Application.Common;
using FluentValidation;

namespace Institutional.Application.Features.Volunteers.CreateVolunteer;

public class CreateVolunteerValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerValidator()
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