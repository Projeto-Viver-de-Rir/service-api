﻿using Ardalis.Result.AspNetCore;
using Institutional.Application.Common.Requests;
using Institutional.Application.Features.Volunteers.CreateVolunteer;
using Institutional.Application.Features.Volunteers.DeleteVolunteer;
using Institutional.Application.Features.Volunteers.GetAllVolunteers;
using Institutional.Application.Features.Volunteers.GetVolunteerById;
using Institutional.Application.Features.Volunteers.UpdateVolunteer;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using Institutional.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace Institutional.Api.Endpoints;

public static class VolunteerEndpoints
{
    public static void MapVolunteerEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/volunteer")
            .WithTags("volunteer")
            .RequireAuthorization();
        
        group.MapGet("/", async (IMediator mediator, [AsParameters] GetAllVolunteersRequest request, UserManager<ApplicationUser> userManager) =>
        {
            var result = await mediator.Send(request);

            foreach (var item in result.Result)
            {
                var id = Guid.Parse(item.AccountId.ToString());
                
                var user = await userManager.Users.SingleOrDefaultAsync(p => p.Id == id);
                item.Email = user?.Email;
                item.Phone = user?.PhoneNumber;
            }
            
            return result;
        });

        group.MapGet("{id}", async (IMediator mediator, VolunteerId id, UserManager<ApplicationUser> userManager) =>
        {
            var result = await mediator.Send(new GetVolunteerByIdRequest(id));

            var userId = Guid.Parse(result.Value.AccountId.ToString());
                
            var user = await userManager.Users.SingleOrDefaultAsync(p => p.Id == userId);
            result.Value.Email = user?.Email;
            result.Value.Phone = user?.PhoneNumber;
            
            return result.ToMinimalApiResult();
        });
        
        group.MapPost("/", async (IMediator mediator, CreateVolunteerRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await mediator.Send(request with { AuditFields = audit });
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", async (IMediator mediator, VolunteerId id, UpdateVolunteerRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { Id = id, AuditFields = audit });
            return result.ToMinimalApiResult();
        });

        group.MapDelete("{id}", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, VolunteerId id, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(new DeleteVolunteerRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}