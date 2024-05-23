using Ardalis.Result;
using Institutional.Application.Common.Requests;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Teams.UpdateTeam;

public record UpdateTeamRequest : IRequest<Result<GetTeamResponse>>
{
    [JsonIgnore]
    public TeamId Id { get; init; }
    
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public TeamType Type { get; init; }
    public TeamStatus Status { get; init; }

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}