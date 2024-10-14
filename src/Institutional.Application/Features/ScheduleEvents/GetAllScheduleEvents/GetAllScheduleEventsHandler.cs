using Institutional.Application.Common;
using Institutional.Application.Common.Responses;
using Institutional.Application.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.ScheduleEvents.GetAllScheduleEvents;

public class GetAllScheduleEventsHandler : IRequestHandler<GetAllScheduleEventsRequest, PaginatedList<GetScheduleEventResponse>>
{
    private readonly IContext _context;
    
    public GetAllScheduleEventsHandler(IContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<GetScheduleEventResponse>> Handle(GetAllScheduleEventsRequest request, CancellationToken cancellationToken)
    {
        var scheduleEvents = _context.ScheduleEvents
            .Include(p => p.Coordinators)
            .WhereIf(!string.IsNullOrEmpty(request.Name),
                x => EF.Functions.Like(x.Name.ToLower(), $"%{request.Name!.ToLower()}%"));
        return await scheduleEvents.ProjectToType<GetScheduleEventResponse>()
            .OrderBy(x => x.Name)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}