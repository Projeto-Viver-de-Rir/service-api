using FluentValidation;

namespace Institutional.Application.Features.Reports.Debts.DeleteItemDebtReport;

public class DeleteItemDebtReportValidator : AbstractValidator<DeleteItemDebtReportRequest>
{

    public DeleteItemDebtReportValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}