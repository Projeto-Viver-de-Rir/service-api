using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MassTransit;
using System;

namespace Boilerplate.Domain.Entities;

public class ScheduleEvent : Entity<ScheduleEventId>
{
    public override ScheduleEventId Id { get; set; } = NewId.NextGuid();
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? MeetingPoint { get; set; }
    public int Occupancy { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public ScheduleEventInterval Occurence { get; set; }
    
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserId? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}