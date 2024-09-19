using Ardalis.Result;
using Institutional.Application.Common;
using Institutional.Domain.Entities;
using Institutional.Domain.Entities.Enums;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Events.ConclusionEvent;

public class ConclusionEventHandler : IRequestHandler<ConclusionEventRequest, Result<GetEventResponse>>
{
    private readonly IContext _context;

    public ConclusionEventHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetEventResponse>> Handle(ConclusionEventRequest request,
        CancellationToken cancellationToken)
    {
        var originalEvent = await _context.Events
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (originalEvent == null || originalEvent.Status != EventStatus.Scheduled) 
            return Result.NotFound();

        if (request.Presences.Any())
        {
            foreach (var volunteerId in request.Presences)
            {
                var originalPresence = await _context.EventPresences
                    .FirstOrDefaultAsync(x => x.EventId == request.Id || x.VolunteerId == volunteerId, cancellationToken);

                if (originalPresence != null)
                {
                    originalPresence.Attended = true;
                    originalPresence.UpdatedAt = request.AuditFields!.StartedAt;
                    originalPresence.UpdatedBy = request.AuditFields!.StartedBy;
                }
            }

            originalEvent.Status = EventStatus.Realized;            
        }
        else
            originalEvent.Status = EventStatus.Canceled;
        
        await _context.SaveChangesAsync(cancellationToken);

        originalEvent.Presences = new List<EventPresence>();
        originalEvent.Coordinators = new List<EventCoordinator>();

        return originalEvent.Adapt<GetEventResponse>();
    }
}