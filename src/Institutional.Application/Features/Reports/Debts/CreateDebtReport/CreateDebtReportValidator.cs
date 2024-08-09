using FluentValidation;

namespace Institutional.Application.Features.Reports.Debts.CreateDebtReport;

public class CreateDebtReportValidator : AbstractValidator<CreateDebtReportRequest>
{
    public CreateDebtReportValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;
    }
}