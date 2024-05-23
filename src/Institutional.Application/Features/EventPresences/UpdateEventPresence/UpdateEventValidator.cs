using Institutional.Application.Common;
using FluentValidation;

namespace Institutional.Application.Features.EventPresences.UpdateEventPresence;

public class UpdateEventPresenceValidator : AbstractValidator<UpdateEventPresenceRequest>
{
    public UpdateEventPresenceValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.EventId)
            .NotEmpty();
        
        RuleFor(x => x.VolunteerId)
            .NotEmpty();
    }
}