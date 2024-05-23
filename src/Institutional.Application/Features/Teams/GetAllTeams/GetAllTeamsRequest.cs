using Institutional.Application.Common.Responses;
using Institutional.Domain.Entities.Enums;
using MediatR;

namespace Institutional.Application.Features.Teams.GetAllTeams;

public record GetAllTeamsRequest
    (string? Name = null, TeamType? TeamType = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetTeamResponse>>;