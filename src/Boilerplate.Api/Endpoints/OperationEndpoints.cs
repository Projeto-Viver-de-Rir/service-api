using Ardalis.Result.AspNetCore;
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
        var group = builder.MapGroup("api/Operation")
            .WithTags("Operation")
            .RequireAuthorization();
        
        group.MapPost("/Events", async (IMediator mediator, CreateScheduleEventRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(request);
            return result.ToMinimalApiResult();
        });
        
        group.MapPost("/Debts", async (IMediator mediator, CreateScheduleEventRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(request);
            return result.ToMinimalApiResult();
        });
    }
}