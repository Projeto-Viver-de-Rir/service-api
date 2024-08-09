using Institutional.Application.Common;
using Institutional.Application.Common.Responses;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Reports.Debts.GetDebtReport;

public class GetDebtReportHandler : IRequestHandler<GetDebtReportRequest, PaginatedList<GetDebtReportResponse>>
{
    private readonly IContext _context;
    
    public GetDebtReportHandler(IContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<GetDebtReportResponse>> Handle(GetDebtReportRequest request, CancellationToken cancellationToken)
    {
        var events = _context.ReportDebts
            .Include(p => p.Volunteer);
        return await events.ProjectToType<GetDebtReportResponse>()
            .OrderBy(x => x.Id)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}