using Ardalis.Result;
using Institutional.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Reports.Debts.DeleteItemDebtReport;

public class DeleteItemDebtReportHandler : IRequestHandler<DeleteItemDebtReportRequest, Result>
{
    private readonly IContext _context;
    public DeleteItemDebtReportHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result> Handle(DeleteItemDebtReportRequest request, CancellationToken cancellationToken)
    {
        var reportDebt = await _context.ReportDebts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (reportDebt is null) 
            return Result.NotFound();
        
        _context.ReportDebts.Remove(reportDebt);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}