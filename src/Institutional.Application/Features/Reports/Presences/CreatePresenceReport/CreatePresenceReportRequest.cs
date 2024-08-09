using Ardalis.Result;
using Institutional.Application.Common.Requests;
using MediatR;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Reports.Presences.CreatePresenceReport;

public record CreatePresenceReportRequest : IRequest<Result<string>>
{
    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}