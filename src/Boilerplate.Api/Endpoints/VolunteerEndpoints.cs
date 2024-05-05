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

namespace Boilerplate.Api.Endpoints;

public static class VolunteerEndpoints
{
    public static void MapVolunteerEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/Volunteer")
            .WithTags("Volunteer");
        
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

        group.MapPost("/", async (IMediator mediator, CreateVolunteerRequest request) =>
        {
            var result = await mediator.Send(request);
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", async (IMediator mediator, VolunteerId id, UpdateVolunteerRequest request) =>
        {
            var result = await mediator.Send(request with { Id = id });
            return result.ToMinimalApiResult();
        });

        group.MapDelete("{id}", async (IMediator mediator, VolunteerId id) =>
        {
            var result = await mediator.Send(new DeleteVolunteerRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}