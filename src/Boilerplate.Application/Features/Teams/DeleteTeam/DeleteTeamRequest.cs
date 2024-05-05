using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Teams.DeleteTeam;

public record DeleteTeamRequest(TeamId Id) : IRequest<Result>;