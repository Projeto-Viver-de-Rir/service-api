using Ardalis.Result;
using Institutional.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Configs.GetConfigById;

public class GetConfigByIdHandler : IRequestHandler<GetConfigByIdRequest, Result<GetConfigResponse>>
{
    private readonly IContext _context;


    public GetConfigByIdHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result<GetConfigResponse>> Handle(GetConfigByIdRequest request, CancellationToken cancellationToken)
    {
        var config = await _context.Configs.FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken: cancellationToken);
        if (config is null) return Result.NotFound();
        return config.Adapt<GetConfigResponse>();
    }
}