using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Teams.GetTeamById;

public record GetTeamByIdRequest(TeamId Id) : IRequest<Result<GetTeamResponse>>;