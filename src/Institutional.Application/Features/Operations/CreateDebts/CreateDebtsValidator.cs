using Institutional.Application.Common;
using FluentValidation;

namespace Institutional.Application.Features.Operations.CreateDebts;

public class CreateDebtsValidator : AbstractValidator<CreateDebtsRequest>
{
    public CreateDebtsValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;
    }
}