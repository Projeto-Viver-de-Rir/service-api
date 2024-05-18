using Boilerplate.Application.Common;
using FluentValidation;

namespace Boilerplate.Application.Features.Debts.PayDebt;

public class PayDebtValidator : AbstractValidator<PayDebtRequest>
{
    public PayDebtValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty();
    }
}