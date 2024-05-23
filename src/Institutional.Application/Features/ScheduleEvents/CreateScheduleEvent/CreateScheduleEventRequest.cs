using Ardalis.Result;
using Institutional.Application.Common.Requests;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.ScheduleEvents.CreateScheduleEvent;

public record CreateScheduleEventRequest : IRequest<Result<GetScheduleEventResponse>>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? MeetingPoint { get; set; }
    public int Occupancy { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public ScheduleEventInterval Occurrence { get; set; }
    public TimeOnly Schedule { get; set; }
    public IEnumerable<VolunteerId>? Coordinators { get; init; } = Enumerable.Empty<VolunteerId>();

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}