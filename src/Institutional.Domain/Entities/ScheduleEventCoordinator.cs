using Institutional.Domain.Entities.Common;
using MassTransit;
using System;

namespace Institutional.Domain.Entities;

public class ScheduleEventCoordinator : Entity<ScheduleEventCoordinatorId>
{
    public override ScheduleEventCoordinatorId Id { get; set; } = NewId.NextGuid();
    public ScheduleEventId ScheduleEventId { get; set; }
    public VolunteerId VolunteerId { get; set; }
    
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public virtual Volunteer Volunteer { get; set; }
}