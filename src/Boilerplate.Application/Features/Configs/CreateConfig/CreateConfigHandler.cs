using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Configs.CreateConfig;

public class CreateConfigHandler : IRequestHandler<CreateConfigRequest, Result<GetConfigResponse>>
{
    private readonly IContext _context;
    
    
    public CreateConfigHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetConfigResponse>> Handle(CreateConfigRequest request, CancellationToken cancellationToken)
    {
        var created = request.Adapt<Domain.Entities.Config>();
        _context.Configs.Add(created);
        await _context.SaveChangesAsync(cancellationToken);
        return created.Adapt<GetConfigResponse>();
    }
}