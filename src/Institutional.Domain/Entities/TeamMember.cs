using Institutional.Domain.Entities.Common;
using MassTransit;
using System;

namespace Institutional.Domain.Entities;

public class TeamMember : Entity<TeamMemberId>
{
    public override TeamMemberId Id { get; set; } = NewId.NextGuid();
    public TeamId TeamId { get; set; }
    public VolunteerId VolunteerId { get; set; }
    
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public virtual Volunteer Volunteer { get; set; }
}