using Institutional.Application.Common.Responses;
using Institutional.Domain.Entities.Enums;
using MediatR;

namespace Institutional.Application.Features.ScheduleEvents.GetAllScheduleEvents;

public record GetAllScheduleEventsRequest
    (string? Name = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetScheduleEventResponse>>;