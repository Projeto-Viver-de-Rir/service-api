using Ardalis.Result;
using Boilerplate.Application.Common.Requests;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.Teams.UpdateTeam;

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