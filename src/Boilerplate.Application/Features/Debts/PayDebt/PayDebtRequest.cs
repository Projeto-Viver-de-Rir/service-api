using Ardalis.Result;
using Boilerplate.Application.Common.Requests;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.Debts.PayDebt;

public record PayDebtRequest : IRequest<Result<GetDebtResponse>>
{
    [JsonIgnore]
    public DebtId Id { get; init; }
    public DateTime? PaidAt { get; init; }

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}