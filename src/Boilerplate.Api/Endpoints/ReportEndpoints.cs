using Boilerplate.Application.Features.Configs.GetAllConfig;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Boilerplate.Api.Endpoints;

public static class ReportEndpoints
{
    public static void MapReportEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/report")
            .WithTags("report")
            .RequireAuthorization();
        
        group.MapGet("/debts", async (IMediator mediator, [AsParameters] GetAllConfigsRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });
        
        group.MapGet("/presences", async (IMediator mediator, [AsParameters] GetAllConfigsRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });
    }
}