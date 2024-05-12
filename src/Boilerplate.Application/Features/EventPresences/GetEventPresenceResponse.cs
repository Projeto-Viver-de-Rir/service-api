using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;

namespace Boilerplate.Application.Features.EventPresences;

public record GetEventPresenceResponse
{
    public EventId EventId { get; set; }
    public VolunteerId VolunteerId { get; set; }
    public DateTime RegistrationAt { get; set; }
    public bool Attended { get; set; }
}