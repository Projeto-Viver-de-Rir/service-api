using Ardalis.Result;
using Institutional.Application.Common;
using Institutional.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Reports.Debts.CreateDebtReport;

public class CreateDebtReportHandler : IRequestHandler<CreateDebtReportRequest, Result<string>>
{
    private readonly IContext _context;
    
    
    public CreateDebtReportHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<string>> Handle(CreateDebtReportRequest request, CancellationToken cancellationToken)
    {
        if (request.AuditFields!.StartedAt.Day < 15)
            return "Este relatório só pode ser gerado a partir do dia 15 de cada mês.";
        
        _context.ReportDebts.RemoveRange(_context.ReportDebts);
        
        var debts = _context.Debts
            .Where(p => !p.PaidAt.HasValue && p.DueDate <= request.AuditFields!.StartedAt)
            .GroupBy(p => p.VolunteerId)
            .Select(p => new ReportDebt()
            {
                VolunteerId = p.Key,
                Quantity = p.Count(),
                Amount = p.Sum(d => d.Amount),
                CreatedAt = request.AuditFields!.StartedAt,
                CreatedBy = request.AuditFields!.StartedBy
            });

        await _context.ReportDebts.AddRangeAsync(debts, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return "Relatório gerado com sucesso.";
    }
}