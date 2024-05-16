using Ardalis.Result;
using Boilerplate.Application.Features.Configs;
using Boilerplate.Application.Common.Requests;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.Configs.UpdateConfig;

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