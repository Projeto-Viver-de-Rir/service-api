using Ardalis.Result;
using Institutional.Application.Common.Requests;
using Institutional.Domain.Entities.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Teams.CreateTeam;

public record CreateTeamRequest : IRequest<Result<GetTeamResponse>>
{
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public TeamType Type { get; init; }
    public TeamStatus Status { get; init; }

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}