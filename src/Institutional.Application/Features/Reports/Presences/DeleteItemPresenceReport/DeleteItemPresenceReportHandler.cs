using Ardalis.Result;
using Institutional.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Reports.Presences.DeleteItemPresenceReport;

public class DeleteItemPresenceReportHandler : IRequestHandler<DeleteItemPresenceReportRequest, Result>
{
    private readonly IContext _context;
    public DeleteItemPresenceReportHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result> Handle(DeleteItemPresenceReportRequest request, CancellationToken cancellationToken)
    {
        var reportPresence = await _context.ReportPresences.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (reportPresence is null) 
            return Result.NotFound();
        
        _context.ReportPresences.Remove(reportPresence);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}