using Ardalis.Result;
using Institutional.Application.Common.Requests;
using MediatR;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Reports.Debts.CreateDebtReport;

public record CreateDebtReportRequest : IRequest<Result<string>>
{
    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}