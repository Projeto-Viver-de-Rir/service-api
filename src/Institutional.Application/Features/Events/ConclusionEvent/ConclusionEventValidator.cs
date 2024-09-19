using Institutional.Application.Common;
using FluentValidation;

namespace Institutional.Application.Features.Events.ConclusionEvent;

public class ConclusionEventValidator : AbstractValidator<ConclusionEventRequest>
{
    public ConclusionEventValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty();
    }
}