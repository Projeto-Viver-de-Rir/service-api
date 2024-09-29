using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using System;

namespace Institutional.Application.Features.Volunteers;

public record GetVolunteerResponse
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
    public UserId AccountId { get; init; }
    public string? Photo { get; set; }
    
    public string? Email { get; set; }
    public string? Phone { get; set; }
    
    public int LastMonthAttendances { get; init; }
    public int ActualMonthAttendances { get; init; }
    public int LastMonthAbsences { get; init; }
    public int ActualMonthAbsences { get; init; }
}