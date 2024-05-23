using Institutional.Application.Common;
using FluentValidation;

namespace Institutional.Application.Features.Operations.CreateEvents;

public class CreateEventsValidator : AbstractValidator<CreateEventsRequest>
{
    public CreateEventsValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;
    }
}