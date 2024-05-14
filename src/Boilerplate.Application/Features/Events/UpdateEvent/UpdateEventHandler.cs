using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Events.UpdateEvent;

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
        await _context.SaveChangesAsync(cancellationToken);
        return originalEvent.Adapt<GetEventResponse>();
    }
}