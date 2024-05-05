using Boilerplate.Application.Common;
using FluentValidation;

namespace Boilerplate.Application.Features.Events.UpdateEvent;

public class UpdateEventValidator : AbstractValidator<UpdateEventRequest>
{
    public UpdateEventValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);
        
        RuleFor(x => x.Description)
            .MaximumLength(StringSizes.Max);
        
        RuleFor(x => x.Address)
            .MaximumLength(StringSizes.Max);

        RuleFor(x => x.City)
            .MaximumLength(StringSizes.Max);

        RuleFor(x => x.MeetingPoint)
            .MaximumLength(StringSizes.Max);
        
        RuleFor(x => x.HappenAt)
            .NotEmpty();
        
        RuleFor(x => x.Occupancy)
            .GreaterThan(0);
        
        RuleFor(x => x.Status)
            .IsInEnum();
    }
}