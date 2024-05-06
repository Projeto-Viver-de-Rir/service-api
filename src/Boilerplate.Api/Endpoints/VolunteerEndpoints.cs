using Ardalis.Result.AspNetCore;
using Boilerplate.Application.Features.Volunteers.CreateVolunteer;
using Boilerplate.Application.Features.Volunteers.DeleteVolunteer;
using Boilerplate.Application.Features.Volunteers.GetAllVolunteers;
using Boilerplate.Application.Features.Volunteers.GetVolunteerById;
using Boilerplate.Application.Features.Volunteers.UpdateVolunteer;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Boilerplate.Api.Endpoints;

public static class VolunteerEndpoints
{
    public static void MapVolunteerEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/Volunteer")
            .WithTags("Volunteer")
            .RequireAuthorization();
        
        group.MapGet("/", async (IMediator mediator, [AsParameters] GetAllVolunteersRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });

        group.MapGet("{id}", async (IMediator mediator, VolunteerId id) =>
        {
            var result = await mediator.Send(new GetVolunteerByIdRequest(id));
            return result.ToMinimalApiResult();
        });

        group.MapPost("/", async (IMediator mediator, CreateVolunteerRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(request);
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", async (IMediator mediator, VolunteerId id, UpdateVolunteerRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(request with { Id = id });
            return result.ToMinimalApiResult();
        });

        group.MapDelete("{id}", async (IMediator mediator, VolunteerId id, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(new DeleteVolunteerRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}