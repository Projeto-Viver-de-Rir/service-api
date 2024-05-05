using FluentValidation;

namespace Boilerplate.Application.Features.Events.DeleteEvent;

public class DeleteEventValidator : AbstractValidator<DeleteEventRequest>
{

    public DeleteEventValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}