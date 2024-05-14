using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Configs.UpdateConfig;

public class UpdateConfigHandler : IRequestHandler<UpdateConfigRequest, Result<GetConfigResponse>>
{
    private readonly IContext _context;

    public UpdateConfigHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetConfigResponse>> Handle(UpdateConfigRequest request,
        CancellationToken cancellationToken)
    {
        var originalConfig = await _context.Configs
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (originalConfig == null) return Result.NotFound();

        originalConfig.Key = request.Key;
        originalConfig.Description = request.Description;
        originalConfig.Value = request.Value;        
        originalConfig.UpdatedBy = request.AuditFields!.StartedBy;
        originalConfig.UpdatedAt = request.AuditFields!.StartedAt;

        _context.Configs.Update(originalConfig);
        await _context.SaveChangesAsync(cancellationToken);
        return originalConfig.Adapt<GetConfigResponse>();
    }
}