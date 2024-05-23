using Ardalis.Result;
using Institutional.Application.Common.Requests;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Debts.PayDebt;

public record PayDebtRequest : IRequest<Result<GetDebtResponse>>
{
    [JsonIgnore]
    public DebtId Id { get; init; }
    public DateTime? PaidAt { get; init; }

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}