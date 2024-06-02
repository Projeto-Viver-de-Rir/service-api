using Institutional.Application.Common.Responses;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Institutional.Application.Features.Teams;

public record GetTeamResponse
{
    public VolunteerId Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public TeamType Type { get; init; }
    public TeamStatus Status { get; init; }
    public IEnumerable<Member>? Members { get; set; }
    
    public UserId CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public UserId? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }    
}

public record Member
{
    public TeamMemberId Id { get; set; }
    public VolunteerWithAccount Volunteer { get; set; }
}