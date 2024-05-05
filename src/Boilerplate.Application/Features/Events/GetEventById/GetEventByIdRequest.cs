using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Events.GetEventById;

public record GetEventByIdRequest(EventId Id) : IRequest<Result<GetEventResponse>>;