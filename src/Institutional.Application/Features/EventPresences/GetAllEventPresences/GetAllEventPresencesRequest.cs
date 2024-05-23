using Institutional.Application.Common.Responses;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;

namespace Institutional.Application.Features.EventPresences.GetAllEventPresences;

public record GetAllEventPresencesRequest
    (string? Name = null, EventId? EventId = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetEventPresenceResponse>>;