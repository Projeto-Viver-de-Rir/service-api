using FluentValidation;

namespace Institutional.Application.Features.Events.DeleteEvent;

public class DeleteEventValidator : AbstractValidator<DeleteEventRequest>
{

    public DeleteEventValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}