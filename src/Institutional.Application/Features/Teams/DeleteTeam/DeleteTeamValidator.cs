using FluentValidation;

namespace Institutional.Application.Features.Teams.DeleteTeam;

public class DeleteTeamValidator : AbstractValidator<DeleteTeamRequest>
{

    public DeleteTeamValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}