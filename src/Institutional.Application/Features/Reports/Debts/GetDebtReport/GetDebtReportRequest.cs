using Institutional.Application.Common.Responses;
using MediatR;

namespace Institutional.Application.Features.Reports.Debts.GetDebtReport;

public record GetDebtReportRequest
    (int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetDebtReportResponse>>;