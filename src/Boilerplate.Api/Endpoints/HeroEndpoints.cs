using Ardalis.Result.AspNetCore;
using Boilerplate.Application.Features.Heroes.CreateHero;
using Boilerplate.Application.Features.Heroes.DeleteHero;
using Boilerplate.Application.Features.Heroes.GetAllHeroes;
using Boilerplate.Application.Features.Heroes.GetHeroById;
using Boilerplate.Application.Features.Heroes.UpdateHero;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;

namespace Boilerplate.Api.Endpoints;

public static class HeroEndpoints
{
    public static void MapHeroEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/Hero")
            .WithTags("Hero")
            .RequireAuthorization();
        
        group.MapGet("/", async (IMediator mediator, [AsParameters] GetAllHeroesRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });

        group.MapGet("{id}", async (IMediator mediator, HeroId id) =>
        {
            var result = await mediator.Send(new GetHeroByIdRequest(id));
            return result.ToMinimalApiResult();
        });

        group.MapPost("/", async (IMediator mediator, CreateHeroRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(request);
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", async (IMediator mediator, HeroId id, UpdateHeroRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(request with { Id = id });
            return result.ToMinimalApiResult();
        });

        group.MapDelete("{id}", async (IMediator mediator, HeroId id) =>
        {
            var result = await mediator.Send(new DeleteHeroRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}