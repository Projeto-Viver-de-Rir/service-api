using FluentValidation;

namespace Boilerplate.Application.Features.Volunteers.DeleteVolunteer;

public class DeleteVolunteerValidator : AbstractValidator<DeleteVolunteerRequest>
{

    public DeleteVolunteerValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}