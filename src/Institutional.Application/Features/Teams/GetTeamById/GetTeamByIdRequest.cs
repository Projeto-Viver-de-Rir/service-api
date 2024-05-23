using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Teams.GetTeamById;

public record GetTeamByIdRequest(TeamId Id) : IRequest<Result<GetTeamResponse>>;