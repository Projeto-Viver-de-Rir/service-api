using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Events.ConfirmPresenceEvent;

public class ConfirmPresenceEventHandler : IRequestHandler<ConfirmPresenceEventRequest, Result<GetEventResponse>>
{
    private readonly IContext _context;

    public ConfirmPresenceEventHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetEventResponse>> Handle(ConfirmPresenceEventRequest request,
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
        
        // _context.Events.Confirm(originalEvent);
        await _context.SaveChangesAsync(cancellationToken);
        return originalEvent.Adapt<GetEventResponse>();
    }
}