using Ardalis.Result;
using Boilerplate.Application.Common.Requests;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.ScheduleEvents.CreateScheduleEvent;

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

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}