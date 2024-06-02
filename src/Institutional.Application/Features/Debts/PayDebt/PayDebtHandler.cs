using Ardalis.Result;
using Institutional.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Debts.PayDebt;

public class PayDebtHandler : IRequestHandler<PayDebtRequest, Result<GetDebtResponse>>
{
    private readonly IContext _context;

    public PayDebtHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetDebtResponse>> Handle(PayDebtRequest request,
        CancellationToken cancellationToken)
    {
        var originalDebt = await _context.Debts
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (originalDebt == null) 
            return Result.NotFound();

        if (!originalDebt.PaidAt.HasValue && request.PaidAt.HasValue)
            originalDebt.PaidBy = request.AuditFields!.StartedBy;
        else if (originalDebt.PaidAt.HasValue && !request.PaidAt.HasValue)
            originalDebt.PaidBy = null;        
        
        originalDebt.PaidAt = request.PaidAt;
        originalDebt.UpdatedBy = request.AuditFields!.StartedBy;
        originalDebt.UpdatedAt = request.AuditFields!.StartedAt;            

        _context.Debts.Update(originalDebt);
        await _context.SaveChangesAsync(cancellationToken);
        return originalDebt.Adapt<GetDebtResponse>();
    }
}