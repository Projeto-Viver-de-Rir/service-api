using Ardalis.Result.AspNetCore;
using Boilerplate.Application.Features.Accounts.GetImpersonate;
using Boilerplate.Application.Features.Accounts.GetMyself;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Security.Claims;

namespace Boilerplate.Api.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/account")
            .WithTags("account")
            .RequireAuthorization();

        group.MapGet("/myself", async (IMediator mediator, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await mediator.Send(new GetMyselfRequest(Guid.Parse(loggedUserId)));
            return result.ToMinimalApiResult();
        });
        
        group.MapGet("/impersonate/{id}", async (IMediator mediator, VolunteerId id, IHttpContextAccessor httpContextAccessor) =>
        {
            var result = await mediator.Send(new GetImpersonateRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}