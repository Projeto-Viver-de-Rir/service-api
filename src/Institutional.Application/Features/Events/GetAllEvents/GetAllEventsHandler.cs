﻿using Institutional.Application.Common;
using Institutional.Application.Common.Responses;
using Institutional.Application.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Events.GetAllEvents;

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
            .Include(p => p.Presences)
            .WhereIf(!string.IsNullOrEmpty(request.Name), x => EF.Functions.Like(x.Name.ToLower(), $"%{request.Name!.ToLower()}%"))
            .WhereIf(request.Status != null, x => x.Status == request.Status);
        
        var paginatedListAsync = await events.ProjectToType<GetEventResponse>()
            .OrderBy(x => x.HappenAt)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
        
        foreach (var eventResponse in paginatedListAsync.Result)
            eventResponse.Capacity = eventResponse.Occupancy - eventResponse.Presences.Count();

        return paginatedListAsync;
    }
}