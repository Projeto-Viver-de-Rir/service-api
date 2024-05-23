using Institutional.Application.Common;
using Institutional.Application.Common.Responses;
using Institutional.Application.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Configs.GetAllConfig;

public class GetAllConfigHandler : IRequestHandler<GetAllConfigsRequest, PaginatedList<GetConfigResponse>>
{
    private readonly IContext _context;
    
    public GetAllConfigHandler(IContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<GetConfigResponse>> Handle(GetAllConfigsRequest request, CancellationToken cancellationToken)
    {
        var configs = _context.Configs
            .WhereIf(request.Type.HasValue, x => x.Type == request.Type);
        return await configs.ProjectToType<GetConfigResponse>()
            .OrderBy(x => x.Type)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}