using Institutional.Application.Common.Responses;
using MediatR;

namespace Institutional.Application.Features.Reports.Presences.GetPresenceReport;

public record GetPresenceReportRequest
    (int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetPresenceReportResponse>>;