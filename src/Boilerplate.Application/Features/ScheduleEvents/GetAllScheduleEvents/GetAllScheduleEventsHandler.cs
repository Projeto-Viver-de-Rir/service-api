﻿using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.ScheduleEvents.GetAllScheduleEvents;

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
            .WhereIf(!string.IsNullOrEmpty(request.Name), x => EF.Functions.Like(x.Name, $"%{request.Name}%"));
        return await scheduleEvents.ProjectToType<GetScheduleEventResponse>()
            .OrderBy(x => x.Name)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}