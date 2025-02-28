﻿using Institutional.Application.Common.Responses;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Institutional.Application.Features.ScheduleEvents;

public record GetScheduleEventResponse
{
    public ScheduleEventId Id { get; init; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? MeetingPoint { get; set; }
    public int Occupancy { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public ScheduleEventInterval Occurrence { get; set; }
    public TimeOnly Schedule { get; set; }
    public IEnumerable<ScheduleCoordinator>? Coordinators { get; set; }
}

public record ScheduleCoordinator
{
    public ScheduleEventCoordinatorId Id { get; set; }
    public VolunteerCard Volunteer { get; set; }
}
