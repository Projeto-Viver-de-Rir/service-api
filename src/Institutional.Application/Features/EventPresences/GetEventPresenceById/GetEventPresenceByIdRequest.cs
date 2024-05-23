using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.EventPresences.GetEventPresenceById;

public record GetEventPresenceByIdRequest(EventPresenceId Id) : IRequest<Result<GetEventPresenceResponse>>;