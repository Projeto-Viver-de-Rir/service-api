using Ardalis.Result;
using Institutional.Application.Common.Requests;
using MediatR;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Configs.CreateConfig;

public record CreateConfigRequest : IRequest<Result<GetConfigResponse>>
{
    public string Key { get; init; } = null!;
    public string? Description { get; init; }
    public string Value { get; init; } = null!;

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}