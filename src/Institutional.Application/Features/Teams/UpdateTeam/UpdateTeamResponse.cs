using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using System.Collections.Generic;

namespace Institutional.Application.Features.Teams.UpdateTeam;

public record UpdateTeamResponse
{
    public VolunteerId Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public TeamType Type { get; init; }
    public TeamStatus Status { get; init; }
    public IEnumerable<UserId>? UsersToRemoveRole { get; set; }
    public IEnumerable<UserId>? UsersToAddRole { get; set; }
}
