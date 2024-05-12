using Boilerplate.Application.Common;
using FluentValidation;

namespace Boilerplate.Application.Features.EventPresences.UpdateEventPresence;

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