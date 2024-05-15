using Ardalis.Result;
using Boilerplate.Application.Common.Requests;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.EventPresences.CreateEventPresence;

public record CreateEventPresenceRequest : IRequest<Result<GetEventPresenceResponse>>
{
    public EventId EventId { get; set; }
    public VolunteerId VolunteerId { get; set; }

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}