using FluentValidation;

namespace Institutional.Application.Features.Reports.Presences.CreatePresenceReport;

public class CreatePresenceReportValidator : AbstractValidator<CreatePresenceReportRequest>
{
    public CreatePresenceReportValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;
    }
}