using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Teams.DeleteTeam;

public record DeleteTeamRequest(TeamId Id) : IRequest<Result>;