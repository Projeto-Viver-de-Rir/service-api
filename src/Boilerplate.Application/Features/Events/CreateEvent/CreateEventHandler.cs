﻿using Ardalis.Result;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Events.CreateEvent;

public class CreateEventHandler : IRequestHandler<CreateEventRequest, Result<GetEventResponse>>
{
    private readonly IContext _context;
    
    
    public CreateEventHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetEventResponse>> Handle(CreateEventRequest request, CancellationToken cancellationToken)
    {
        var created = request.Adapt<Domain.Entities.Event>();
        created.CreatedBy = request.AuditFields!.StartedBy;
        created.CreatedAt = request.AuditFields!.StartedAt;

        _context.Events.Add(created);

        if (request.Coordinators != null)
        {
            foreach (var item in request.Coordinators)
            {
                var eventCoordinator = new EventCoordinator()
                {
                    EventId = created.Id,
                    VolunteerId = item,
                    CreatedBy = request.AuditFields!.StartedBy,
                    CreatedAt = request.AuditFields!.StartedAt
                };

                _context.EventCoordinators.Add(eventCoordinator);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        var response = created.Adapt<GetEventResponse>();
        //response.Coordinators = request.Coordinators;
        
        return response;
    }
}