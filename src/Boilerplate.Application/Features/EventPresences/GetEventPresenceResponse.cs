using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;

namespace Boilerplate.Application.Features.EventPresences;

public record GetEventPresenceResponse
{
    public EventPresenceId Id { get; set; }
    public EventId EventId { get; set; }
    public VolunteerId VolunteerId { get; set; }
    public string Name { get; set; } = "Volunteer Cool Name";
    public string Photo { get; set; }
    public DateTime RegistrationAt { get; set; }
    public bool Attended { get; set; }
}