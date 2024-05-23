using Institutional.Application.Common.Responses;
using Institutional.Domain.Entities.Enums;
using MediatR;

namespace Institutional.Application.Features.Events.GetAllEvents;

public record GetAllEventsRequest
    (string? Name = null, EventStatus? Status = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetEventResponse>>;