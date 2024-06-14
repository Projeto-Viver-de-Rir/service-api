using Institutional.Domain.Entities.Common;
using System;
using System.Collections.Generic;

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
    public string? Country { get; init; }
    public DateTime? BirthDate { get; init; }
    public string? Availability { get; init; }
    public string? Comments { get; init; }
    public string? Identifier { get; init; }
    public string? Photo { get; init; }
    
    public string? Email { get; init; }
    public string? Phone { get; init; }
    
    public int LastMonthAttendances { get; init; }
    public int ActualMonthAttendances { get; init; }
    public int LastMonthAbsences { get; init; }
    public int ActualMonthAbsences { get; init; }

    public IEnumerable<string>? Permissions { get; init; } =
        new[] { "volunteer", "fiscal", "advisory", "legal", "operational", "administrative" };
}