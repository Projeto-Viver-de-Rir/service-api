using Ardalis.Result;
using Institutional.Application.Common.Requests;
using MediatR;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Accounts.Account;

public record AccountRequest : IRequest<Result<GetMyselfResponse>>
{
    public string Email { get; init; } = null!;
    public string Phone { get; init; } = null!;
    
    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}