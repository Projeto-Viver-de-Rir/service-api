using Ardalis.Result.AspNetCore;
using Boilerplate.Application.Common.Requests;
using Boilerplate.Application.Features.ScheduleEvents.CreateScheduleEvent;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Boilerplate.Api.Endpoints;

public static class OperationEndpoints
{
    public static void MapOperationEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/operation")
            .WithTags("operation")
            .RequireAuthorization();
        
        group.MapPost("/events", async (IMediator mediator, CreateScheduleEventRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { AuditFields = audit });
            return result.ToMinimalApiResult();
        });
        
        group.MapPost("/debts", async (IMediator mediator, CreateScheduleEventRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { AuditFields = audit });
            return result.ToMinimalApiResult();
        });
    }
}