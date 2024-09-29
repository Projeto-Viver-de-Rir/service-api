using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MassTransit;
using System;
using System.Collections.Generic;

namespace Institutional.Domain.Entities;

public class Event : Entity<EventId>
{
    public override EventId Id { get; set; } = NewId.NextGuid();
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? MeetingPoint { get; set; }
    public DateTime? HappenAt { get; set; }
    public int Occupancy { get; set; }
    public EventStatus Status { get; set; }
    public ScheduleEventId ScheduleEventId { get; set; }
    
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserId? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public virtual ICollection<EventPresence> Presences { get; set; }
    public virtual ICollection<EventCoordinator> Coordinators { get; set; }
}