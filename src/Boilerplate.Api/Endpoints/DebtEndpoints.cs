using Ardalis.Result.AspNetCore;
using Boilerplate.Application.Features.Debts.CreateDebt;
using Boilerplate.Application.Features.Debts.DeleteDebt;
using Boilerplate.Application.Features.Debts.GetAllDebts;
using Boilerplate.Application.Features.Debts.GetDebtById;
using Boilerplate.Application.Features.Debts.UpdateDebt;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Boilerplate.Api.Endpoints;

public static class DebtEndpoints
{
    public static void MapDebtEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/Debt")
            .WithTags("Debt")
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

        group.MapPost("/", async (IMediator mediator, CreateDebtRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(request);
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", async (IMediator mediator, DebtId id, UpdateDebtRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(request with { Id = id });
            return result.ToMinimalApiResult();
        });

        group.MapDelete("{id}", async (IMediator mediator, DebtId id, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await mediator.Send(new DeleteDebtRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}