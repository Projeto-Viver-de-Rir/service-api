using Ardalis.Result;
using Institutional.Application.Common.Requests;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Accounts.EnrollAccount;

public record EnrollAccountRequest : IRequest<Result<GetMyselfResponseV2>>
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
    public string? Identifier { get; init; }
    public string Photo { get; init; }
    
    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}