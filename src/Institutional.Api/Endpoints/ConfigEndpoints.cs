using Ardalis.Result.AspNetCore;
using Institutional.Application.Common.Requests;
using Institutional.Application.Features.Configs.CreateConfig;
using Institutional.Application.Features.Configs.DeleteConfig;
using Institutional.Application.Features.Configs.GetAllConfig;
using Institutional.Application.Features.Configs.GetConfigById;
using Institutional.Application.Features.Configs.UpdateConfig;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Institutional.Api.Endpoints;

public static class ConfigEndpoints
{
    public static void MapConfigEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/config")
            .WithTags("config")
            .RequireAuthorization();
        
        group.MapGet("/", [Authorize(Roles = $"{nameof(TeamType.Administrative)}")] async (IMediator mediator, [AsParameters] GetAllConfigsRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });

        group.MapGet("{id}", async (IMediator mediator, ConfigId id) =>
        {
            var result = await mediator.Send(new GetConfigByIdRequest(id));
            return result.ToMinimalApiResult();
        });

        group.MapPost("/", [Authorize(Roles = $"{nameof(TeamType.Administrative)}")] async (IMediator mediator, CreateConfigRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { AuditFields = audit });
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", [Authorize(Roles = $"{nameof(TeamType.Administrative)}")] async (IMediator mediator, ConfigId id, UpdateConfigRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { Id = id, AuditFields = audit });
            return result.ToMinimalApiResult();
        });
        
        group.MapDelete("{id}", [Authorize(Roles = $"{nameof(TeamType.Administrative)}")] async (IMediator mediator, ConfigId id) =>
        {
            var result = await mediator.Send(new DeleteConfigRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}