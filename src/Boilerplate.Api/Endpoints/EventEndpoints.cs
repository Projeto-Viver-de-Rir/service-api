using Ardalis.Result.AspNetCore;
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

namespace Boilerplate.Api.Endpoints;

public static class EventEndpoints
{
    public static void MapEventEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/Event")
            .WithTags("Event");
        
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

        group.MapPost("/", async (IMediator mediator, CreateEventRequest request) =>
        {
            var result = await mediator.Send(request);
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", async (IMediator mediator, EventId id, UpdateEventRequest request) =>
        {
            var result = await mediator.Send(request with { Id = id });
            return result.ToMinimalApiResult();
        });

        group.MapDelete("{id}", async (IMediator mediator, EventId id) =>
        {
            var result = await mediator.Send(new DeleteEventRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}