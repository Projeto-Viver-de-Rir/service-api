﻿using Institutional.Application.Common;
using FluentValidation;

namespace Institutional.Application.Features.ScheduleEvents.UpdateScheduleEvent;

public class UpdateScheduleEventValidator : AbstractValidator<UpdateScheduleEventRequest>
{
    public UpdateScheduleEventValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty();
        
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