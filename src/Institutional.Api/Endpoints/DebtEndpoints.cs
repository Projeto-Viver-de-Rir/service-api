using Ardalis.Result.AspNetCore;
using Institutional.Application.Common.Requests;
using Institutional.Application.Features.Debts.CreateDebt;
using Institutional.Application.Features.Debts.DeleteDebt;
using Institutional.Application.Features.Debts.GetAllDebts;
using Institutional.Application.Features.Debts.GetDebtById;
using Institutional.Application.Features.Debts.PayDebt;
using Institutional.Application.Features.Debts.UpdateDebt;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Institutional.Api.Endpoints;

public static class DebtEndpoints
{
    public static void MapDebtEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/debt")
            .WithTags("debt")
            .RequireAuthorization();
        
        group.MapGet("/", async (IMediator mediator, [AsParameters] GetAllDebtsRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });

        group.MapGet("{id}", async (IMediator mediator, DebtId id) =>
        {
            var result = await mediator.Send(new GetDebtByIdRequest(id));
            return result.ToMinimalApiResult();
        });

        group.MapPost("/", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Fiscal)}")] async (IMediator mediator, CreateDebtRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { AuditFields = audit });
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", [Authorize(Roles = $"{nameof(TeamType.Administrative)}")] async (IMediator mediator, DebtId id, UpdateDebtRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { Id = id, AuditFields = audit });
            return result.ToMinimalApiResult();
        });

        group.MapPatch("{id}", [Authorize(Roles = $"{nameof(TeamType.Administrative)}")] async (IMediator mediator, DebtId id, PayDebtRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var result = await mediator.Send(request with { Id = id, AuditFields = audit });
            return result.ToMinimalApiResult();
        });        
        
        group.MapDelete("{id}", [Authorize(Roles = $"{nameof(TeamType.Administrative)},{nameof(TeamType.Fiscal)}")] async (IMediator mediator, DebtId id, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(new DeleteDebtRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}