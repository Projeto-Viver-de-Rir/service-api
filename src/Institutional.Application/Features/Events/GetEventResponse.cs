﻿using Institutional.Application.Common.Responses;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Institutional.Application.Features.Events;

public record GetEventResponse
{
    public EventId Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public string? Address { get; init; }
    public string? City { get; init; }
    public string? MeetingPoint { get; init; }
    public DateTime? HappenAt { get; init; }
    public int Occupancy { get; init; }
    public EventStatus Status { get; init; }
    public IEnumerable<Coordinator>? Coordinators { get; set; }
    
    // TODO: Return correct capacity (Occupancy - ListOfEventPresence.Count())
    public int Capacity { get; init; }
}

public record Coordinator
{
    public EventCoordinatorId Id { get; set; }
    public VolunteerCard Volunteer { get; set; }
}
