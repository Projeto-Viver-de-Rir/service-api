using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;

namespace Boilerplate.Application.Features.EventPresences;

public record GetEventPresenceResponse
{
    public EventPresenceId Id { get; set; }
    public EventId EventId { get; set; }
    public DateTime RegistrationAt { get; set; }
    public bool Attended { get; set; }
    
    public Volunteer Volunteer { get; set; }
}

public record Volunteer
{
    public VolunteerId Id { get; set; }
    public string Name { get; set; }
    public string Photo { get; set; }
}
