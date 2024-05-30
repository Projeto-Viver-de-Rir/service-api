using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MassTransit;
using System;
using System.Collections.Generic;

namespace Institutional.Domain.Entities;

public class Team : Entity<TeamId>
{
    public override TeamId Id { get; set; } = NewId.NextGuid();
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public TeamType Type { get; set; }
    public TeamStatus Status { get; set; }
    public virtual ICollection<TeamMember> Members { get; set; }
    
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserId? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}