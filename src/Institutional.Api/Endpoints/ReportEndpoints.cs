using Institutional.Application.Common.Requests;
using Institutional.Application.Features.Reports.Debts.CreateDebtReport;
using Institutional.Application.Features.Reports.Debts.DeleteItemDebtReport;
using Institutional.Application.Features.Reports.Debts.GetDebtReport;
using Institutional.Application.Features.Reports.Presences.CreatePresenceReport;
using Institutional.Application.Features.Reports.Presences.DeleteItemPresenceReport;
using Institutional.Application.Features.Reports.Presences.GetPresenceReport;
using Institutional.Domain.Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Institutional.Api.Endpoints;

public static class ReportEndpoints
{
    public static void MapReportEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/report")
            .WithTags("report")
            .RequireAuthorization();
        
        group.MapPost("/debts", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Fiscal)}")] async (IMediator mediator, [AsParameters] CreateDebtReportRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { AuditFields = audit });
            return result;
        });
        
        group.MapGet("/debts", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Fiscal)}")] async (IMediator mediator, [AsParameters] GetDebtReportRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });
        
        group.MapDelete("/debts/{id}", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Fiscal)}")] async (IMediator mediator, [AsParameters] DeleteItemDebtReportRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });
        
        group.MapPost("/presences", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, [AsParameters] CreatePresenceReportRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { AuditFields = audit });
            return result;
        });
        
        group.MapGet("/presences", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, [AsParameters] GetPresenceReportRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });
        
        group.MapDelete("/presences/{id}", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Operational)}")] async (IMediator mediator, [AsParameters] DeleteItemPresenceReportRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });
    }
}