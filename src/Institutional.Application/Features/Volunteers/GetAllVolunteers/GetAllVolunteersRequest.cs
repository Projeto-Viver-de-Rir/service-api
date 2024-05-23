using Institutional.Application.Common.Responses;
using Institutional.Domain.Entities.Enums;
using MediatR;

namespace Institutional.Application.Features.Volunteers.GetAllVolunteers;

public record GetAllVolunteersRequest
    (string? Name = null, string? Nickname = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetVolunteerResponse>>;