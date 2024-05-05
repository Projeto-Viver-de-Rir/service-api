using Boilerplate.Application.Common.Responses;
using Boilerplate.Domain.Entities.Enums;
using MediatR;

namespace Boilerplate.Application.Features.Events.GetAllEvents;

public record GetAllEventsRequest
    (string? Name = null, EventStatus? Status = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetEventResponse>>;