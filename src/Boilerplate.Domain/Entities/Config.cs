using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MassTransit;
using System;

namespace Boilerplate.Domain.Entities;

public class Config : Entity<ConfigId>
{
    public override ConfigId Id { get; set; } = NewId.NextGuid();
    public ConfigType Type { get; set; }
    public string? Description { get; set; }
    public string Value { get; set; } = null!;
    
    public UserId? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}