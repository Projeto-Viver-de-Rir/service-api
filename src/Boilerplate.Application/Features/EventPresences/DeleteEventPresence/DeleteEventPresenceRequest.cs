using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.EventPresences.DeleteEventPresence;

public record DeleteEventPresenceRequest(EventPresenceId Id) : IRequest<Result>;