using Institutional.Application.Common;
using FluentValidation;

namespace Institutional.Application.Features.Debts.PayDebt;

public class PayDebtValidator : AbstractValidator<PayDebtRequest>
{
    public PayDebtValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty();
    }
}