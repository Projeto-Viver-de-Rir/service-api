using Ardalis.Result;
using Boilerplate.Application.Common.Requests;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.Teams.CreateTeam;

public record CreateTeamRequest : IRequest<Result<GetTeamResponse>>
{
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public TeamType Type { get; init; }
    public TeamStatus Status { get; init; }

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}