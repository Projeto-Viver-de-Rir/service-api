using Institutional.Application.Common;
using FluentValidation;

namespace Institutional.Application.Features.Teams.CreateTeam;

public class CreateTeamValidator : AbstractValidator<CreateTeamRequest>
{
    public CreateTeamValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);

        RuleFor(x => x.Type)
            .IsInEnum();
        
        RuleFor(x => x.Status)
            .IsInEnum();
    }
}