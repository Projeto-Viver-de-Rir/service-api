using Ardalis.Result.AspNetCore;
using Boilerplate.Application.Features.ScheduleEvents.CreateScheduleEvent;
using Boilerplate.Application.Features.ScheduleEvents.DeleteScheduleEvent;
using Boilerplate.Application.Features.ScheduleEvents.GetAllScheduleEvents;
using Boilerplate.Application.Features.ScheduleEvents.GetScheduleEventById;
using Boilerplate.Application.Features.ScheduleEvents.UpdateScheduleEvent;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Boilerplate.Api.Endpoints;

public static class ScheduleScheduleEventEndpoints
{
    public static void MapScheduleEventEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/ScheduleEvent")
            .WithTags("ScheduleEvent")
            .RequireAuthorization();
        
        group.MapGet("/", async (IMediator mediator, [AsParameters] GetAllScheduleEventsRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });

        group.MapGet("{id}", async (IMediator mediator, ScheduleEventId id) =>
        {
            var result = await mediator.Send(new GetScheduleEventByIdRequest(id));
            return result.ToMinimalApiResult();
        });

        group.MapPost("/", async (IMediator mediator, CreateScheduleEventRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(request);
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", async (IMediator mediator, ScheduleEventId id, UpdateScheduleEventRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(request with { Id = id });
            return result.ToMinimalApiResult();
        });
        
        group.MapDelete("{id}", async (IMediator mediator, ScheduleEventId id, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(new DeleteScheduleEventRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}