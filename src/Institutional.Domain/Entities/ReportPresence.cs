using Institutional.Domain.Entities.Common;
using MassTransit;
using System;

namespace Institutional.Domain.Entities;

public class ReportPresence : Entity<ReportPresenceId>
{
    public override ReportPresenceId Id { get; set; } = NewId.NextGuid();
    public VolunteerId VolunteerId { get; set; }
    public int PreviousMonthAttendance { get; set; }
    public int LastMonthAttendance { get; set; }
    
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public virtual Volunteer Volunteer { get; set; }
}