using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.EventPresences.UpdateEventPresence;

public record UpdateEventPresenceRequest : IRequest<Result<GetEventPresenceResponse>>
{
    [JsonIgnore]
    public EventPresenceId Id { get; init; }
    
    public EventId EventId { get; set; }
    public VolunteerId VolunteerId { get; set; }
    public DateTime RegistrationAt { get; set; }
    public bool Attended { get; set; }
}