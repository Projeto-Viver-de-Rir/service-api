using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.ScheduleEvents.GetScheduleEventById;

public record GetScheduleEventByIdRequest(ScheduleEventId Id) : IRequest<Result<GetScheduleEventResponse>>;