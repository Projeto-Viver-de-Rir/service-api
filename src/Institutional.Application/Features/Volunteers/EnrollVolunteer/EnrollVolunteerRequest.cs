using Ardalis.Result;
using Institutional.Application.Common.Requests;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Volunteers.EnrollVolunteer;

public record EnrollVolunteerRequest : IRequest<Result<GetVolunteerResponse>>
{
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
    public string? Photo { get; init; }
    
    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}