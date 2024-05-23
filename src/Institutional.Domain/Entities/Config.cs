using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MassTransit;
using System;

namespace Institutional.Domain.Entities;

public class Config : Entity<ConfigId>
{
    public override ConfigId Id { get; set; } = NewId.NextGuid();
    public ConfigType Type { get; set; }
    public string? Description { get; set; }
    public string Value { get; set; } = null!;
    
    public UserId? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}