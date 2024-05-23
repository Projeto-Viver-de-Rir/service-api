using FluentValidation;

namespace Institutional.Application.Features.Volunteers.DeleteVolunteer;

public class DeleteVolunteerValidator : AbstractValidator<DeleteVolunteerRequest>
{

    public DeleteVolunteerValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}