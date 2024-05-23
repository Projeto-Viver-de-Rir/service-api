using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using System;

namespace Institutional.Application.Features.Debts;

public record GetDebtResponse
{
    public DebtId Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public decimal Amount { get; init; }
    public DateTime DueDate { get; init; }
    public VolunteerId VolunteerId { get; init; }
    public DateTime? PaidAt { get; init; }
    public UserId? PaidBy { get; init; }
}