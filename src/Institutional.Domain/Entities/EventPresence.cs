using Institutional.Domain.Entities.Common;
using MassTransit;
using System;

namespace Institutional.Domain.Entities;

public class EventPresence : Entity<EventPresenceId>
{
    public override EventPresenceId Id { get; set; } = NewId.NextGuid();
    public EventId EventId { get; set; }
    public VolunteerId VolunteerId { get; set; }
    public DateTime RegistrationAt { get; set; }
    public bool Attended { get; set; }
    
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserId? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Volunteer Volunteer { get; set; }
    public virtual Event Event { get; set; }
}