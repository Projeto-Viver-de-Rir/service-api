using Ardalis.Result.AspNetCore;
using Institutional.Application.Common.Requests;
using Institutional.Application.Features.Operations.CreateDebts;
using Institutional.Application.Features.Operations.CreateEvents;
using Institutional.Domain.Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Extensions;
using System.Security.Claims;

namespace Institutional.Api.Endpoints;

public static class OperationEndpoints
{
    public static void MapOperationEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/operation")
            .WithTags("operation")
            .RequireAuthorization();
        
        group.MapPost("/events", [Authorize(Roles = $"{nameof(TeamType.Administrative)}")] async (IMediator mediator, CreateEventsRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { AuditFields = audit });
            return result.ToMinimalApiResult();
        });
        
        group.MapPost("/debts", [Authorize(Roles = $"{nameof(TeamType.Administrative)}")] async (IMediator mediator, CreateDebtsRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { AuditFields = audit });
            return result.ToMinimalApiResult();
        });
    }
}