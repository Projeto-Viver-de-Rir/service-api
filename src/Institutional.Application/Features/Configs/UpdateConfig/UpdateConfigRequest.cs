using Ardalis.Result;
using Institutional.Application.Features.Configs;
using Institutional.Application.Common.Requests;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Configs.UpdateConfig;

public record UpdateConfigRequest : IRequest<Result<GetConfigResponse>>
{
    [JsonIgnore]
    public ConfigId Id { get; init; }
    
    public ConfigType Type { get; init; }
    public string? Description { get; init; }
    public string Value { get; init; } = null!;

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}