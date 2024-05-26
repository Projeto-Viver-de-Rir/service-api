using Ardalis.Result.AspNetCore;
using Institutional.Application.Common.Requests;
using Institutional.Application.Features.EventPresences.CreateEventPresence;
using Institutional.Application.Features.EventPresences.DeleteEventPresence;
using Institutional.Application.Features.EventPresences.GetAllEventPresences;
using Institutional.Application.Features.EventPresences.GetEventPresenceById;
using Institutional.Application.Features.EventPresences.UpdateEventPresence;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Institutional.Api.Endpoints;

public static class EventPresenceEndpoints
{
    public static void MapEventPresenceEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/event-presence")
            .WithTags("event-presence")
            .RequireAuthorization();
        
        group.MapGet("/", async (IMediator mediator, [AsParameters] GetAllEventPresencesRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });

        group.MapGet("{id}", async (IMediator mediator, EventPresenceId id) =>
        {
            var result = await mediator.Send(new GetEventPresenceByIdRequest(id));
            return result.ToMinimalApiResult();
        });

        group.MapPost("/", async (IMediator mediator, CreateEventPresenceRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { AuditFields = audit });
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, EventPresenceId id, UpdateEventPresenceRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { Id = id, AuditFields = audit });
            return result.ToMinimalApiResult();
        });

        group.MapDelete("{id}", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, EventPresenceId id, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(new DeleteEventPresenceRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}