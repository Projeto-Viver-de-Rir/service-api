using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Events.GetAllEvents;

public class GetAllEventsHandler : IRequestHandler<GetAllEventsRequest, PaginatedList<GetEventResponse>>
{
    private readonly IContext _context;
    
    public GetAllEventsHandler(IContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<GetEventResponse>> Handle(GetAllEventsRequest request, CancellationToken cancellationToken)
    {
        var events = _context.Events
            .Include(p => p.Coordinators)
            .WhereIf(!string.IsNullOrEmpty(request.Name), x => EF.Functions.Like(x.Name, $"%{request.Name}%"))
            .WhereIf(request.Status != null, x => x.Status == request.Status);
        return await events.ProjectToType<GetEventResponse>()
            .OrderBy(x => x.Name)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}