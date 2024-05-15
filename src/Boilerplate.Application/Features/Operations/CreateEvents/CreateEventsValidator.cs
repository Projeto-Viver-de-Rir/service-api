using Boilerplate.Application.Common;
using FluentValidation;

namespace Boilerplate.Application.Features.Operations.CreateEvents;

public class CreateEventsValidator : AbstractValidator<CreateEventsRequest>
{
    public CreateEventsValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;
    }
}