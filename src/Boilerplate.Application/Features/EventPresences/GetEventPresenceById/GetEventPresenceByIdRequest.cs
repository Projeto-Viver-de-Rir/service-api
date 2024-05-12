using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.EventPresences.GetEventPresenceById;

public record GetEventPresenceByIdRequest(EventPresenceId Id) : IRequest<Result<GetEventPresenceResponse>>;