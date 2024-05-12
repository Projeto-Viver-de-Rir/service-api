using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;

namespace Boilerplate.Application.Features.Configs;

public record GetConfigResponse
{
    public ConfigId Id { get; init; }
    public string Key { get; init; } = null!;
    public string? Description { get; init; }
    public string Value { get; init; }
    
    public UserId CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public UserId? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }    
}