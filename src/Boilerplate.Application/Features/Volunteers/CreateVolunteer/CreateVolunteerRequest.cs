using Ardalis.Result;
using Boilerplate.Application.Common.Requests;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest : IRequest<Result<GetVolunteerResponse>>
{
    public string Name { get; init; } = null!;
    public string? Nickname { get; init; }
    public string? Address { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? Zip { get; init; }
    public DateTime? BirthDate { get; init; }
    public string? Availability { get; init; }
    public string? Comments { get; init; }
    
    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}