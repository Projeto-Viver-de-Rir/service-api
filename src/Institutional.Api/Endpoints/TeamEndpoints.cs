using Ardalis.Result.AspNetCore;
using Institutional.Application.Common.Requests;
using Institutional.Application.Features.Teams.CreateTeam;
using Institutional.Application.Features.Teams.DeleteTeam;
using Institutional.Application.Features.Teams.GetAllTeams;
using Institutional.Application.Features.Teams.GetTeamById;
using Institutional.Application.Features.Teams.UpdateTeam;
using Institutional.Domain.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Institutional.Api.Endpoints;

public static class TeamEndpoints
{
    public static void MapTeamEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/team")
            .WithTags("team")
            .RequireAuthorization();
        
        group.MapGet("/", async (IMediator mediator, [AsParameters] GetAllTeamsRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });

        group.MapGet("{id}", async (IMediator mediator, TeamId id) =>
        {
            var result = await mediator.Send(new GetTeamByIdRequest(id));
            return result.ToMinimalApiResult();
        });

        group.MapPost("/", async (IMediator mediator, CreateTeamRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { AuditFields = audit });
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", async (IMediator mediator, TeamId id, UpdateTeamRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { Id = id, AuditFields = audit });
            return result.ToMinimalApiResult();
        });

        group.MapDelete("{id}", async (IMediator mediator, TeamId id) =>
        {
            var result = await mediator.Send(new DeleteTeamRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}