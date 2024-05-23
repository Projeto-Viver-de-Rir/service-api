using Institutional.Application.Common;
using FluentValidation;

namespace Institutional.Application.Features.EventPresences.CreateEventPresence;

public class CreatePresenceEventValidator : AbstractValidator<CreateEventPresenceRequest>
{
    public CreatePresenceEventValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        RuleFor(x => x.EventId)
            .NotEmpty();
        
        RuleFor(x => x.VolunteerId)
            .NotEmpty();
    }
}