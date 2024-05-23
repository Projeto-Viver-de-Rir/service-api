using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.EventPresences.DeleteEventPresence;

public record DeleteEventPresenceRequest(EventPresenceId Id) : IRequest<Result>;