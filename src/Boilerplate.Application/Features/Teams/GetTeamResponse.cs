using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;

namespace Boilerplate.Application.Features.Teams;

public record GetTeamResponse
{
    public VolunteerId Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public TeamType Type { get; init; }
    public TeamStatus Status { get; init; }
    
    public UserId CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public UserId? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }    
}