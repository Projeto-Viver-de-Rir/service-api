using Ardalis.Result;
using Institutional.Application.Common;
using Institutional.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Events.UpdateEvent;

public class UpdateEventHandler : IRequestHandler<UpdateEventRequest, Result<GetEventResponse>>
{
    private readonly IContext _context;

    public UpdateEventHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetEventResponse>> Handle(UpdateEventRequest request,
        CancellationToken cancellationToken)
    {
        var originalEvent = await _context.Events
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (originalEvent == null) return Result.NotFound();

        originalEvent.Name = request.Name;
        originalEvent.Description = request.Description;
        originalEvent.Address = request.Address;
        originalEvent.City = request.City;
        originalEvent.MeetingPoint = request.MeetingPoint;
        originalEvent.HappenAt = request.HappenAt;
        originalEvent.Occupancy = request.Occupancy;
        originalEvent.Status = request.Status;
        originalEvent.UpdatedBy = request.AuditFields!.StartedBy;
        originalEvent.UpdatedAt = request.AuditFields!.StartedAt;

        _context.Events.Update(originalEvent);
        
        var originalCoordinators = await _context.EventCoordinators
            .Where(p => p.EventId == request.Id).Select(p => p.VolunteerId).ToListAsync(cancellationToken);
        
        if (request.Coordinators != null)
        {
            var coordinatorsToInsert = request.Coordinators.Except(originalCoordinators);
            
            foreach (var item in coordinatorsToInsert)
            {
                var coordinator = new EventCoordinator()
                {
                    EventId = request.Id,
                    VolunteerId = item,
                    CreatedBy = request.AuditFields!.StartedBy,
                    CreatedAt = request.AuditFields!.StartedAt
                };
                
                _context.EventCoordinators.Add(coordinator);
            }
        }
        
        if (request.Coordinators != null)
        {
            var coordinatorsToRemove = originalCoordinators.Except(request.Coordinators);

            foreach (var item in coordinatorsToRemove)
            {
                var coordinator = await _context.EventCoordinators
                    .Where(p => p.VolunteerId == item && p.EventId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                
                _context.EventCoordinators.Remove(coordinator);
            }
        }
        
        await _context.SaveChangesAsync(cancellationToken);
        
        originalEvent.Presences = new List<EventPresence>();
        originalEvent.Coordinators = new List<EventCoordinator>();
        
        return originalEvent.Adapt<GetEventResponse>();
    }
}