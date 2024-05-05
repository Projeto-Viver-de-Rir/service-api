using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Debts.UpdateDebt;

public class UpdateDebtHandler : IRequestHandler<UpdateDebtRequest, Result<GetDebtResponse>>
{
    private readonly IContext _context;

    public UpdateDebtHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetDebtResponse>> Handle(UpdateDebtRequest request,
        CancellationToken cancellationToken)
    {
        var originalDebt = await _context.Debts
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (originalDebt == null) return Result.NotFound();

        originalDebt.Name = request.Name;
        originalDebt.Description = request.Description;
        originalDebt.Amount = request.Amount;
        originalDebt.DueDate = request.DueDate;
        originalDebt.VolunteerId = request.VolunteerId;
        originalDebt.PaidAt = request.PaidAt;
        originalDebt.PaidBy = request.PaidBy;
        
        _context.Debts.Update(originalDebt);
        await _context.SaveChangesAsync(cancellationToken);
        return originalDebt.Adapt<GetDebtResponse>();
    }
}