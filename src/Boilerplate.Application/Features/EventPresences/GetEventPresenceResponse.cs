using Boilerplate.Application.Common.Responses;
using Boilerplate.Domain.Entities.Common;
using System;

namespace Boilerplate.Application.Features.EventPresences;

public record GetEventPresenceResponse
{
    public EventPresenceId Id { get; set; }
    public EventId EventId { get; set; }
    public DateTime RegistrationAt { get; set; }
    public bool Attended { get; set; }
    
    public VolunteerCard Volunteer { get; set; }
}