using FluentValidation;

namespace Institutional.Application.Features.EventPresences.DeleteEventPresence;

public class DeleteEventPresenceValidator : AbstractValidator<DeleteEventPresenceRequest>
{

    public DeleteEventPresenceValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}