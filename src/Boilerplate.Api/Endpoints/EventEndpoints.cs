using Ardalis.Result.AspNetCore;
using Boilerplate.Application.Common.Requests;
using Boilerplate.Application.Features.Events.ConclusionEvent;
using Boilerplate.Application.Features.Events.ConfirmPresenceEvent;
using Boilerplate.Application.Features.Events.CreateEvent;
using Boilerplate.Application.Features.Events.DeleteEvent;
using Boilerplate.Application.Features.Events.GetAllEvents;
using Boilerplate.Application.Features.Events.GetEventById;
using Boilerplate.Application.Features.Events.UpdateEvent;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Boilerplate.Api.Endpoints;

public static class EventEndpoints
{
    public static void MapEventEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/Event")
            .WithTags("Event")
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

        group.MapPost("/", async (IMediator mediator, CreateEventRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(request);
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", async (IMediator mediator, EventId id, UpdateEventRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(request with { Id = id });
            return result.ToMinimalApiResult();
        });

        group.MapDelete("{id}", async (IMediator mediator, EventId id, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(new DeleteEventRequest(id));
            return result.ToMinimalApiResult();
        });
        
        group.MapPut("{id}/Confirm", async (IMediator mediator, EventId id, ConfirmPresenceEventRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit = 
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { Id = id });
            return result.ToMinimalApiResult();
        });
        
        group.MapPut("{id}/Conclusion", async (IMediator mediator, EventId id, ConclusionEventRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit = 
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { Id = id, AuditFields = audit});
            return result.ToMinimalApiResult();
        });
    }
}