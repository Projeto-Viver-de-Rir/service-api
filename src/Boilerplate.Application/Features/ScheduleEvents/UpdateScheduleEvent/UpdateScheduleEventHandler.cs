using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.ScheduleEvents.UpdateScheduleEvent;

public class UpdateScheduleEventHandler : IRequestHandler<UpdateScheduleEventRequest, Result<GetScheduleEventResponse>>
{
    private readonly IContext _context;

    public UpdateScheduleEventHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetScheduleEventResponse>> Handle(UpdateScheduleEventRequest request,
        CancellationToken cancellationToken)
    {
        var originalScheduleEvent = await _context.ScheduleEvents
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (originalScheduleEvent == null) return Result.NotFound();

        originalScheduleEvent.Name = request.Name;
        originalScheduleEvent.Description = request.Description;
        originalScheduleEvent.Address = request.Address;
        originalScheduleEvent.City = request.City;
        originalScheduleEvent.MeetingPoint = request.MeetingPoint;
        originalScheduleEvent.Occupancy = request.Occupancy;
        originalScheduleEvent.DayOfWeek = request.DayOfWeek;
        originalScheduleEvent.Occurence = request.Occurrence;
        originalScheduleEvent.UpdatedBy = request.AuditFields!.StartedBy;
        originalScheduleEvent.UpdatedAt = request.AuditFields!.StartedAt;
        
        _context.ScheduleEvents.Update(originalScheduleEvent);
        await _context.SaveChangesAsync(cancellationToken);
        return originalScheduleEvent.Adapt<GetScheduleEventResponse>();
    }
}