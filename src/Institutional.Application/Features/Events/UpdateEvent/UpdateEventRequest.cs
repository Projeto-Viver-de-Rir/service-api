using Ardalis.Result;
using Institutional.Application.Common.Requests;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Events.UpdateEvent;

public record UpdateEventRequest : IRequest<Result<GetEventResponse>>
{
    [JsonIgnore]
    public EventId Id { get; init; }
    
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public string? Address { get; init; }
    public string? City { get; init; }
    public string? MeetingPoint { get; init; }
    public DateTime? HappenAt { get; init; }
    public int Occupancy { get; init; }
    public EventStatus Status { get; init; }
    public IEnumerable<VolunteerId>? Coordinators { get; init; } = Enumerable.Empty<VolunteerId>();

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}