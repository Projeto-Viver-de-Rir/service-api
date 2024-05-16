using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Operations.CreateDebts;

public class CreateDebtsHandler : IRequestHandler<CreateDebtsRequest, Result<GetOperationsResponse>>
{
    private readonly IContext _context;
    
    
    public CreateDebtsHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetOperationsResponse>> Handle(CreateDebtsRequest request, CancellationToken cancellationToken)
    {
        var created = request.Adapt<Domain.Entities.Config>();

        _context.Configs.Add(created);
        await _context.SaveChangesAsync(cancellationToken);
        return created.Adapt<GetOperationsResponse>();
    }
}