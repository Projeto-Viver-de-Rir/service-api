using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Debts.DeleteDebt;

public record DeleteDebtRequest(DebtId Id) : IRequest<Result>;