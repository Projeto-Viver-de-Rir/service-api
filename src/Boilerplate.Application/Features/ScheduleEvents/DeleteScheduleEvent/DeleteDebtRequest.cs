using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.ScheduleEvents.DeleteScheduleEvent;

public record DeleteScheduleEventRequest(ScheduleEventId Id) : IRequest<Result>;