using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Debts.CreateDebt;

public class CreateDebtHandler : IRequestHandler<CreateDebtRequest, Result<GetDebtResponse>>
{
    private readonly IContext _context;
    
    
    public CreateDebtHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetDebtResponse>> Handle(CreateDebtRequest request, CancellationToken cancellationToken)
    {
        var created = request.Adapt<Domain.Entities.Hero>();
        _context.Heroes.Add(created);
        await _context.SaveChangesAsync(cancellationToken);
        return created.Adapt<GetDebtResponse>();
    }
}