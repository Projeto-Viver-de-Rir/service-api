using Boilerplate.Application.Common.Responses;
using Boilerplate.Domain.Entities.Enums;
using MediatR;

namespace Boilerplate.Application.Features.ScheduleEvents.GetAllScheduleEvents;

public record GetAllScheduleEventsRequest
    (string? Name = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetScheduleEventResponse>>;