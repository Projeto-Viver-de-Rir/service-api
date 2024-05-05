using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Debts.GetDebtById;

public record GetDebtByIdRequest(DebtId Id) : IRequest<Result<GetDebtResponse>>;