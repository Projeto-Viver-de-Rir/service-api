using FluentValidation;

namespace Boilerplate.Application.Features.Debts.DeleteDebt;

public class DeleteDebtValidator : AbstractValidator<DeleteDebtRequest>
{

    public DeleteDebtValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}