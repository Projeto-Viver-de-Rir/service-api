using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.ScheduleEvents.DeleteScheduleEvent;

public record DeleteScheduleEventRequest(ScheduleEventId Id) : IRequest<Result>;