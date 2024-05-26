using Ardalis.Result.AspNetCore;
using Institutional.Application.Common.Requests;
using Institutional.Application.Features.Events.ConclusionEvent;
using Institutional.Application.Features.Events.CreateEvent;
using Institutional.Application.Features.Events.DeleteEvent;
using Institutional.Application.Features.Events.GetAllEvents;
using Institutional.Application.Features.Events.GetEventById;
using Institutional.Application.Features.Events.UpdateEvent;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Institutional.Api.Endpoints;

public static class EventEndpoints
{
    public static void MapEventEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/event")
            .WithTags("event")
            .RequireAuthorization();
        
        group.MapGet("/", async (IMediator mediator, [AsParameters] GetAllEventsRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });

        group.MapGet("{id}", async (IMediator mediator, EventId id) =>
        {
            var result = await mediator.Send(new GetEventByIdRequest(id));
            return result.ToMinimalApiResult();
        });

        group.MapPost("/", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, CreateEventRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { AuditFields = audit});
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, EventId id, UpdateEventRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { Id = id, AuditFields = audit });
            return result.ToMinimalApiResult();
        });

        group.MapDelete("{id}", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, EventId id, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(new DeleteEventRequest(id));
            return result.ToMinimalApiResult();
        });
        
        group.MapPut("{id}/conclusion", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, EventId id, ConclusionEventRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit = 
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { Id = id, AuditFields = audit});
            return result.ToMinimalApiResult();
        });
    }
}