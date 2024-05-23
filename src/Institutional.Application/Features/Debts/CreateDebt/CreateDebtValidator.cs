using Institutional.Application.Common;
using FluentValidation;

namespace Institutional.Application.Features.Debts.CreateDebt;

public class CreateDebtValidator : AbstractValidator<CreateDebtRequest>
{
    public CreateDebtValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);
        
        RuleFor(x => x.Amount)
            .GreaterThan(0);
        
        RuleFor(x => x.DueDate)
            .NotEmpty();
        
        RuleFor(x => x.VolunteerId)
            .NotEmpty();
    }
}