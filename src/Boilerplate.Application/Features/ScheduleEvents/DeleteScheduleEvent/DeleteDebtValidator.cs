using FluentValidation;

namespace Boilerplate.Application.Features.ScheduleEvents.DeleteScheduleEvent;

public class DeleteScheduleEventValidator : AbstractValidator<DeleteScheduleEventRequest>
{

    public DeleteScheduleEventValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}