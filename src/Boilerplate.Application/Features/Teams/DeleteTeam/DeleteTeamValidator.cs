using FluentValidation;

namespace Boilerplate.Application.Features.Teams.DeleteTeam;

public class DeleteTeamValidator : AbstractValidator<DeleteTeamRequest>
{

    public DeleteTeamValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}