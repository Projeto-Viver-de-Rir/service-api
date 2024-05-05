using Boilerplate.Application.Common;
using FluentValidation;

namespace Boilerplate.Application.Features.Debts.UpdateDebt;

public class UpdateDebtValidator : AbstractValidator<UpdateDebtRequest>
{
    public UpdateDebtValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty();
        
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