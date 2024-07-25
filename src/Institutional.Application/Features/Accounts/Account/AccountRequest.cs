using Ardalis.Result;
using Institutional.Application.Common.Requests;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Accounts.Account;

public record AccountRequest : IRequest<Result<GetMyselfResponse>>
{
    public string FullName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Phone { get; init; } = null!;
    
    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}