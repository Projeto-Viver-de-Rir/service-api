using Ardalis.Result;
using Boilerplate.Domain.Entities.Enums;
using MediatR;

namespace Boilerplate.Application.Features.Teams.CreateTeam;

public record CreateTeamRequest : IRequest<Result<GetTeamResponse>>
{
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public TeamType Type { get; init; }
    public TeamStatus Status { get; init; }
}