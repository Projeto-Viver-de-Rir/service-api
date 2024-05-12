using Boilerplate.Domain.Entities.Common;
using MassTransit;
using System;

namespace Boilerplate.Domain.Entities;

public class Config : Entity<ConfigId>
{
    public override ConfigId Id { get; set; } = NewId.NextGuid();
    public string Key { get; set; } = null!;
    public string? Description { get; set; }
    public string Value { get; set; } = null!;
    
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserId? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}