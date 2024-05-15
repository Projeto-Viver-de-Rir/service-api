using Boilerplate.Application.Common;
using FluentValidation;

namespace Boilerplate.Application.Features.Operations.CreateDebts;

public class CreateDebtsValidator : AbstractValidator<CreateDebtsRequest>
{
    public CreateDebtsValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;
    }
}