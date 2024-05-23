using Ardalis.Result;
using Institutional.Application.Common.Requests;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.EventPresences.UpdateEventPresence;

public record UpdateEventPresenceRequest : IRequest<Result<GetEventPresenceResponse>>
{
    [JsonIgnore]
    public EventPresenceId Id { get; init; }
    
    public EventId EventId { get; set; }
    public VolunteerId VolunteerId { get; set; }
    public bool Attended { get; set; }

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}