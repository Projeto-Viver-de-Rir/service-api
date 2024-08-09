using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Reports.Presences.DeleteItemPresenceReport;

public record DeleteItemPresenceReportRequest(ReportPresenceId Id) : IRequest<Result>;