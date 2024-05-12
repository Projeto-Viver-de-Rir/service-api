using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.ScheduleEvents.GetScheduleEventById;

public record GetScheduleEventByIdRequest(ScheduleEventId Id) : IRequest<Result<GetScheduleEventResponse>>;