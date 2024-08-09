using FluentValidation;

namespace Institutional.Application.Features.Reports.Presences.DeleteItemPresenceReport;

public class DeleteItemPresenceReportValidator : AbstractValidator<DeleteItemPresenceReportRequest>
{

    public DeleteItemPresenceReportValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}