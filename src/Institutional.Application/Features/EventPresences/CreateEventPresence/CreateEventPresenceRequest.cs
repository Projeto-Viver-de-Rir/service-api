using Ardalis.Result;
using Institutional.Application.Common.Requests;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.EventPresences.CreateEventPresence;

public record CreateEventPresenceRequest : IRequest<Result<GetEventPresenceResponse>>
{
    public EventId EventId { get; set; }
    public VolunteerId VolunteerId { get; set; }

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}