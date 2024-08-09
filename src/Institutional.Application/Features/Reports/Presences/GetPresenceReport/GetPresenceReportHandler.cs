using Institutional.Application.Common;
using Institutional.Application.Common.Responses;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Reports.Presences.GetPresenceReport;

public class GetPresenceReportHandler : IRequestHandler<GetPresenceReportRequest, PaginatedList<GetPresenceReportResponse>>
{
    private readonly IContext _context;
    
    public GetPresenceReportHandler(IContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<GetPresenceReportResponse>> Handle(GetPresenceReportRequest request, CancellationToken cancellationToken)
    {
        var events = _context.ReportPresences
            .Include(p => p.Volunteer);
        return await events.ProjectToType<GetPresenceReportResponse>()
            .OrderBy(x => x.Id)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}