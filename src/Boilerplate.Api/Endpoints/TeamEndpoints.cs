using Ardalis.Result.AspNetCore;
using Boilerplate.Application.Features.Teams.CreateTeam;
using Boilerplate.Application.Features.Teams.DeleteTeam;
using Boilerplate.Application.Features.Teams.GetAllTeams;
using Boilerplate.Application.Features.Teams.GetTeamById;
using Boilerplate.Application.Features.Teams.UpdateTeam;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Boilerplate.Api.Endpoints;

public static class TeamEndpoints
{
    public static void MapTeamEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/Team")
            .WithTags("Team");
        
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

        group.MapPost("/", async (IMediator mediator, CreateTeamRequest request) =>
        {
            var result = await mediator.Send(request);
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", async (IMediator mediator, TeamId id, UpdateTeamRequest request) =>
        {
            var result = await mediator.Send(request with { Id = id });
            return result.ToMinimalApiResult();
        });

        group.MapDelete("{id}", async (IMediator mediator, TeamId id) =>
        {
            var result = await mediator.Send(new DeleteTeamRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}