using Institutional.Application.Features.Configs.GetAllConfig;
using Institutional.Domain.Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Institutional.Api.Endpoints;

public static class ReportEndpoints
{
    public static void MapReportEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/report")
            .WithTags("report")
            .RequireAuthorization();
        
        group.MapGet("/debts", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Fiscal)}")] async (IMediator mediator, [AsParameters] GetAllConfigsRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });
        
        group.MapGet("/presences", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, [AsParameters] GetAllConfigsRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });
    }
}