using Ardalis.Result;
using Institutional.Application.Common;
using Institutional.Domain.Entities;
using Institutional.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Operations.CreateEvents;

public class CreateEventsHandler : IRequestHandler<CreateEventsRequest, Result<GetOperationsResponse>>
{
    private readonly IContext _context;
    
    
    public CreateEventsHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetOperationsResponse>> Handle(CreateEventsRequest request, CancellationToken cancellationToken)
    {
        var scheduleEvents = _context.ScheduleEvents.Select(p => p).ToImmutableList();

        var daysInMonth = DateTime.DaysInMonth(request.MonthToGenerate.Year, request.MonthToGenerate.Month);
        var firstDayOfMonth = new DateTime(request.MonthToGenerate.Year, request.MonthToGenerate.Month, 1, 0, 0, 0, kind: DateTimeKind.Utc);
        
        var days = 
            Enumerable.Range(0, daysInMonth).
            Select(value => firstDayOfMonth.AddDays(value));
        
        var allPossibleEvents = from schedule in scheduleEvents
            join day in days on schedule.DayOfWeek equals day.DayOfWeek
            select new
            {
                Name = schedule.Name,
                Description = schedule.Description,
                Address = schedule.Address,
                City = schedule.City,
                MeetingPoint = schedule.MeetingPoint,
                DayToOccur = day,
                Occupancy = schedule.Occupancy,
                DayOfWeek = schedule.DayOfWeek,
                Occurrence = schedule.Occurrence,
                Schedule = schedule.Schedule,
                Id = schedule.Id
            };
        
        var eventsAvailableWithPosition =
            allPossibleEvents
            .GroupBy(item => item.Name )
            .SelectMany(grouping =>
                grouping.OrderBy(item => item.DayToOccur )
                    .Select((schedule, rowCount) => 
                        new
                        {
                            Name = schedule.Name,
                            Description = schedule.Description,
                            Address = schedule.Address,
                            City = schedule.City,
                            MeetingPoint = schedule.MeetingPoint,
                            DayToOccur = schedule.DayToOccur,
                            Occupancy = schedule.Occupancy,
                            DayOfWeek = schedule.DayOfWeek,
                            Occurrence = schedule.Occurrence,
                            Schedule = schedule.Schedule,
                            Id = schedule.Id,
                            PositionWithinMonth = rowCount + 1 
                        }));
        
        var eventsWeeklyOrSpecific =
            eventsAvailableWithPosition
            .Where(item => 
                    item.Occurrence == ScheduleEventInterval.EveryWeekDay || 
                    (
                        item.Occurrence != ScheduleEventInterval.EveryWeekDay && item.Occurrence != ScheduleEventInterval.LastOccurenceWeekDay && 
                        (int)item.Occurrence == item.PositionWithinMonth)
                    )
            .Select(item => new Event
            {
                Name = item.Name,
                Description = item.Description,
                Address = item.Address,
                City = item.City,
                MeetingPoint = item.MeetingPoint,
                HappenAt = new DateTime(item.DayToOccur.Year, 
                    item.DayToOccur.Month, 
                    item.DayToOccur.Day, 
                    item.Schedule.Hour, 
                    item.Schedule.Minute, 
                    0,
                    kind: DateTimeKind.Utc),
                Occupancy = item.Occupancy,
                Status = EventStatus.Scheduled,
                ScheduleEventId = item.Id,
                CreatedAt = request.AuditFields!.StartedAt,
                CreatedBy = request.AuditFields!.StartedBy
            });
        
        var eventsLastOccurence = eventsAvailableWithPosition
            .Where(item => item.Occurrence == ScheduleEventInterval.LastOccurenceWeekDay)
            .GroupBy(groupItem => new
            {
                Name = groupItem.Name,
                Description = groupItem.Description,
                Address = groupItem.Address,
                City = groupItem.City,
                MeetingPoint = groupItem.MeetingPoint,
                Occupancy = groupItem.Occupancy,
                DayOfWeek = groupItem.DayOfWeek,
                Occurrence = groupItem.Occurrence,
                Schedule = groupItem.Schedule,
                ScheduleEventId = groupItem.Id
            })
            .SelectMany(grouping => grouping.Where(b => b.DayToOccur == grouping.Max(c => c.DayToOccur)))
            .Select(item => new Event
            {
                Name = item.Name,
                Description = item.Description,
                Address = item.Address,
                City = item.City,
                MeetingPoint = item.MeetingPoint,
                HappenAt = new DateTime(item.DayToOccur.Year, 
                    item.DayToOccur.Month, 
                    item.DayToOccur.Day, 
                    item.Schedule.Hour, 
                    item.Schedule.Minute, 
                    0,
                    kind: DateTimeKind.Utc),
                Occupancy = item.Occupancy,
                Status = EventStatus.Scheduled,
                ScheduleEventId = item.Id,
                CreatedAt = request.AuditFields!.StartedAt,
                CreatedBy = request.AuditFields!.StartedBy                
            });

        var events = eventsWeeklyOrSpecific.Union(eventsLastOccurence);
        
        await _context.Events.AddRangeAsync(events, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        var scheduleEventCoordinators = 
            _context.ScheduleEventCoordinators
                .Select(p => new { p.ScheduleEventId, p.VolunteerId}).ToImmutableList();

        var createdEvents = _context.Events.Where(p => p.CreatedAt == request.AuditFields!.StartedAt).ToImmutableList();
        
        var coordinators = from p in createdEvents
            join c in scheduleEventCoordinators
            on p.ScheduleEventId equals c.ScheduleEventId
            select new EventCoordinator()
            {
                EventId = p.Id,
                VolunteerId = c.VolunteerId,
                CreatedAt = request.AuditFields!.StartedAt,
                CreatedBy = request.AuditFields!.StartedBy
            };
        
        await _context.EventCoordinators.AddRangeAsync(coordinators, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return new GetOperationsResponse()
        {
            BaseItems = scheduleEvents.Count(), 
            GeneratedItems = events.Count()
        };
    }
}