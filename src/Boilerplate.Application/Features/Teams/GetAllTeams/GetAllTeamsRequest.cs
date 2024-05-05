using Boilerplate.Application.Common.Responses;
using Boilerplate.Domain.Entities.Enums;
using MediatR;

namespace Boilerplate.Application.Features.Teams.GetAllTeams;

public record GetAllTeamsRequest
    (string? Name = null, TeamType? TeamType = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetTeamResponse>>;