﻿using Ardalis.Result;
using Institutional.Application.Common.Requests;
using MediatR;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Accounts.Account;

public record AccountRequest : IRequest<Result<string>>
{
    public string Email { get; init; } = null!;
    public string Phone { get; init; } = null!;
    
    public ChangePassword? ChangePassword { get; init; } = null!;
    
    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}

public record ChangePassword
{
    public string CurrentPassword { get; init; } = null!;
    public string NewPassword { get; init; } = null!;
}