using Boilerplate.Application.Common;
using FluentValidation;

namespace Boilerplate.Application.Features.ScheduleEvents.CreateScheduleEvent;

public class CreateScheduleEventValidator : AbstractValidator<CreateScheduleEventRequest>
{
    public CreateScheduleEventValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);
    }
}