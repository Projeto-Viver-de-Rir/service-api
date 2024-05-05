using Ardalis.Result;
using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Debts.DeleteDebt;

public class DeleteDebtHandler : IRequestHandler<DeleteDebtRequest, Result>
{
    private readonly IContext _context;
    public DeleteDebtHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result> Handle(DeleteDebtRequest request, CancellationToken cancellationToken)
    {
        var debt = await _context.Debts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (debt is null) return Result.NotFound();
        _context.Debts.Remove(debt);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}