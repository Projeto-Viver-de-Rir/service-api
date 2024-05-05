using Boilerplate.Application.Common.Responses;
using Boilerplate.Domain.Entities.Enums;
using MediatR;

namespace Boilerplate.Application.Features.Volunteers.GetAllVolunteers;

public record GetAllVolunteersRequest
    (string? Name = null, string? Nickname = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetVolunteerResponse>>;