using Boilerplate.Application.Common;
using FluentValidation;

namespace Boilerplate.Application.Features.Events.ConclusionEvent;

public class ConclusionEventValidator : AbstractValidator<ConclusionEventRequest>
{
    public ConclusionEventValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Presences)
            .NotEmpty();
    }
}