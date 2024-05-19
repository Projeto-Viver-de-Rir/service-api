﻿using Ardalis.Result;
using Boilerplate.Application.Common.Requests;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.Debts.CreateDebt;

public record CreateDebtRequest : IRequest<Result<GetDebtResponse>>
{
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public decimal Amount { get; init; }
    public DateTime DueDate { get; init; }
    public VolunteerId VolunteerId { get; init; }
    public DateTime? PaidAt { get; init; }

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}