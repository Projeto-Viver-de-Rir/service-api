using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Events.DeleteEvent;

public record DeleteEventRequest(EventId Id) : IRequest<Result>;