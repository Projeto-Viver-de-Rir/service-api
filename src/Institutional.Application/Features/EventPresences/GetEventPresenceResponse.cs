using Institutional.Application.Common.Responses;
using Institutional.Domain.Entities.Common;
using System;

namespace Institutional.Application.Features.EventPresences;

public record GetEventPresenceResponse
{
    public EventPresenceId Id { get; set; }
    public EventId EventId { get; set; }
    public DateTime RegistrationAt { get; set; }
    public bool Attended { get; set; }
    
    public VolunteerCard Volunteer { get; set; }
}