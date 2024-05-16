using Ardalis.Result;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities.Enums;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Events.ConclusionEvent;

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
        
        if (originalEvent == null) 
            return Result.NotFound();

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
        
        await _context.SaveChangesAsync(cancellationToken);
        return originalEvent.Adapt<GetEventResponse>();
    }
}