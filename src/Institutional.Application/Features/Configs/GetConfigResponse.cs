using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using System;

namespace Institutional.Application.Features.Configs;

public record GetConfigResponse
{
    public ConfigId Id { get; init; }
    public ConfigType Type { get; init; }
    public string? Description { get; init; }
    public string Value { get; init; }
    
    public UserId? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }    
}