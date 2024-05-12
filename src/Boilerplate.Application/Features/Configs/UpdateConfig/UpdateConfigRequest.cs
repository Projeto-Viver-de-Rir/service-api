using Ardalis.Result;
using Boilerplate.Application.Features.Configs;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.Configs.UpdateConfig;

public record UpdateConfigRequest : IRequest<Result<GetConfigResponse>>
{
    [JsonIgnore]
    public ConfigId Id { get; init; }
    
    public string Key { get; init; } = null!;
    public string? Description { get; init; }
    public string Value { get; init; } = null!;
}