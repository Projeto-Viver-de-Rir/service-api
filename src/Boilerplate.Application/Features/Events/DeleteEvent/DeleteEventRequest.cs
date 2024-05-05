using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Events.DeleteEvent;

public record DeleteEventRequest(EventId Id) : IRequest<Result>;