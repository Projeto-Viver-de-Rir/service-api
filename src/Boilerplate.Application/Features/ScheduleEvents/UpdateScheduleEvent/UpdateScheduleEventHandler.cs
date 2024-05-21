using Ardalis.Result;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        originalScheduleEvent.Occurrence = request.Occurrence;
        originalScheduleEvent.UpdatedBy = request.AuditFields!.StartedBy;
        originalScheduleEvent.UpdatedAt = request.AuditFields!.StartedAt;
        
        _context.ScheduleEvents.Update(originalScheduleEvent);
        
        var originalCoordinators = await _context.ScheduleEventCoordinators
            .Where(p => p.ScheduleEventId == request.Id).Select(p => p.VolunteerId).ToListAsync(cancellationToken);
        
        if (request.Coordinators != null)
        {
            var coordinatorsToInsert = request.Coordinators.Except(originalCoordinators);
            
            foreach (var item in coordinatorsToInsert)
            {
                var coordinator = new ScheduleEventCoordinator()
                {
                    ScheduleEventId = request.Id,
                    VolunteerId = item,
                    CreatedBy = request.AuditFields!.StartedBy,
                    CreatedAt = request.AuditFields!.StartedAt
                };
                
                _context.ScheduleEventCoordinators.Add(coordinator);
            }
        }
        
        if (request.Coordinators != null)
        {
            var coordinatorsToRemove = originalCoordinators.Except(request.Coordinators);

            foreach (var item in coordinatorsToRemove)
            {
                var coordinator = await _context.ScheduleEventCoordinators
                    .Where(p => p.VolunteerId == item && p.ScheduleEventId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                
                _context.ScheduleEventCoordinators.Remove(coordinator);
            }
        }
        
        await _context.SaveChangesAsync(cancellationToken);

        var response = originalScheduleEvent.Adapt<GetScheduleEventResponse>();
        //response.Coordinators = request.Coordinators;
        
        return response;
    }
}