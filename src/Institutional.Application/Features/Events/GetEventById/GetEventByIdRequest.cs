using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Events.GetEventById;

public record GetEventByIdRequest(EventId Id) : IRequest<Result<GetEventResponse>>;