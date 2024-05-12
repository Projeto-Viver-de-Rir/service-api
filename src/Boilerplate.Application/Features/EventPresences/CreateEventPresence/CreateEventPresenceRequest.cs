using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;

namespace Boilerplate.Application.Features.EventPresences.CreateEventPresence;

public record CreateEventPresenceRequest : IRequest<Result<GetEventPresenceResponse>>
{
    public EventId EventId { get; set; }
    public VolunteerId VolunteerId { get; set; }
    public DateTime RegistrationAt { get; set; }
    public bool Attended { get; set; }
}