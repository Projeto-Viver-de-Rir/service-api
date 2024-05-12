using Ardalis.Result.AspNetCore;
using Boilerplate.Application.Features.EventPresences.CreateEventPresence;
using Boilerplate.Application.Features.EventPresences.DeleteEventPresence;
using Boilerplate.Application.Features.EventPresences.GetAllEventPresences;
using Boilerplate.Application.Features.EventPresences.GetEventPresenceById;
using Boilerplate.Application.Features.EventPresences.UpdateEventPresence;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Boilerplate.Api.Endpoints;

public static class EventPresenceEndpoints
{
    public static void MapEventPresenceEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/EventPresence")
            .WithTags("EventPresence")
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
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(request);
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", async (IMediator mediator, EventPresenceId id, UpdateEventPresenceRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(request with { Id = id });
            return result.ToMinimalApiResult();
        });

        group.MapDelete("{id}", async (IMediator mediator, EventPresenceId id, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(new DeleteEventPresenceRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}