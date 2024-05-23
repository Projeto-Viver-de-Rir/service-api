using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Debts.DeleteDebt;

public record DeleteDebtRequest(DebtId Id) : IRequest<Result>;