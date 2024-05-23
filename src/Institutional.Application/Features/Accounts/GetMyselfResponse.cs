using Institutional.Domain.Entities.Common;
using System;

namespace Institutional.Application.Features.Accounts;

public record GetMyselfResponse
{
    public VolunteerId Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Nickname { get; init; }
    public string? Address { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? Zip { get; init; }
    public DateTime? BirthDate { get; init; }
    public string? Availability { get; init; }
    public string? Comments { get; init; }

    public int LastMonthAttendances { get; init; }
    public int ActualMonthAttendances { get; init; }

    public int LastMonthAbsences { get; init; }
    public int ActualMonthAbsences { get; init; }
}