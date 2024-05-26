using Ardalis.Result.AspNetCore;
using Institutional.Application.Common.Requests;
using Institutional.Application.Features.ScheduleEvents.CreateScheduleEvent;
using Institutional.Application.Features.ScheduleEvents.DeleteScheduleEvent;
using Institutional.Application.Features.ScheduleEvents.GetAllScheduleEvents;
using Institutional.Application.Features.ScheduleEvents.GetScheduleEventById;
using Institutional.Application.Features.ScheduleEvents.UpdateScheduleEvent;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Institutional.Api.Endpoints;

public static class ScheduleScheduleEventEndpoints
{
    public static void MapScheduleEventEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/schedule-event")
            .WithTags("schedule-event")
            .RequireAuthorization();
        
        group.MapGet("/", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, [AsParameters] GetAllScheduleEventsRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });

        group.MapGet("{id}", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, ScheduleEventId id) =>
        {
            var result = await mediator.Send(new GetScheduleEventByIdRequest(id));
            return result.ToMinimalApiResult();
        });

        group.MapPost("/", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, CreateScheduleEventRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { AuditFields = audit });
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, ScheduleEventId id, UpdateScheduleEventRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { Id = id, AuditFields = audit });
            return result.ToMinimalApiResult();
        });
        
        group.MapDelete("{id}", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, ScheduleEventId id, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(new DeleteScheduleEventRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}