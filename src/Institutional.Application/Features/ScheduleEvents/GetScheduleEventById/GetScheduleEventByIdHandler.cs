﻿using Ardalis.Result;
using Institutional.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.ScheduleEvents.GetScheduleEventById;

public class GetScheduleEventByIdHandler : IRequestHandler<GetScheduleEventByIdRequest, Result<GetScheduleEventResponse>>
{
    private readonly IContext _context;


    public GetScheduleEventByIdHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result<GetScheduleEventResponse>> Handle(GetScheduleEventByIdRequest request, CancellationToken cancellationToken)
    {
        var debt = await _context.ScheduleEvents
            .Include(p => p.Coordinators)
            .FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken: cancellationToken);
        
        if (debt is null) 
            return Result.NotFound();
        
        return debt.Adapt<GetScheduleEventResponse>();
    }
}