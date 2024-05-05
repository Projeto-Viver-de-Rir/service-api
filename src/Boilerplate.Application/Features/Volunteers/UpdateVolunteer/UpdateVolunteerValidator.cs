using Boilerplate.Application.Common;
using FluentValidation;

namespace Boilerplate.Application.Features.Volunteers.UpdateVolunteer;

public class UpdateVolunteerValidator : AbstractValidator<UpdateVolunteerRequest>
{
    public UpdateVolunteerValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);
        
        RuleFor(x => x.Nickname)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);
    }
}