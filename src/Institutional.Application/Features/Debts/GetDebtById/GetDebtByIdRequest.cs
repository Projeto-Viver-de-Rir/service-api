using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Debts.GetDebtById;

public record GetDebtByIdRequest(DebtId Id) : IRequest<Result<GetDebtResponse>>;