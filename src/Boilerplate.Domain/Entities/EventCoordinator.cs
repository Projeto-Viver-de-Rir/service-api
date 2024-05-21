﻿using Boilerplate.Domain.Entities.Common;
using MassTransit;
using System;

namespace Boilerplate.Domain.Entities;

public class EventCoordinator : Entity<EventCoordinatorId>
{
    public override EventCoordinatorId Id { get; set; } = NewId.NextGuid();
    public EventId EventId { get; set; }
    public VolunteerId VolunteerId { get; set; }
    
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public virtual Volunteer Volunteer { get; set; }
}