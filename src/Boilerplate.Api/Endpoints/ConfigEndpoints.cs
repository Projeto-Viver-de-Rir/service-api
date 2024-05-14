using Ardalis.Result.AspNetCore;
using Boilerplate.Application.Common.Requests;
using Boilerplate.Application.Features.Configs.CreateConfig;
using Boilerplate.Application.Features.Configs.DeleteConfig;
using Boilerplate.Application.Features.Configs.GetAllConfig;
using Boilerplate.Application.Features.Configs.GetConfigById;
using Boilerplate.Application.Features.Configs.UpdateConfig;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Boilerplate.Api.Endpoints;

public static class ConfigEndpoints
{
    public static void MapConfigEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/config")
            .WithTags("config")
            .RequireAuthorization();
        
        group.MapGet("/", async (IMediator mediator, [AsParameters] GetAllConfigsRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });

        group.MapGet("{id}", async (IMediator mediator, ConfigId id) =>
        {
            var result = await mediator.Send(new GetConfigByIdRequest(id));
            return result.ToMinimalApiResult();
        });

        group.MapPost("/", async (IMediator mediator, CreateConfigRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { AuditFields = audit });
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", async (IMediator mediator, ConfigId id, UpdateConfigRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { Id = id, AuditFields = audit });
            return result.ToMinimalApiResult();
        });
        
        group.MapDelete("{id}", async (IMediator mediator, ConfigId id) =>
        {
            var result = await mediator.Send(new DeleteConfigRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}