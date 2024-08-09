using Institutional.Domain.Entities.Common;
using MassTransit;
using System;

namespace Institutional.Domain.Entities;

public class ReportDebt : Entity<ReportDebtId>
{
    public override ReportDebtId Id { get; set; } = NewId.NextGuid();
    public VolunteerId VolunteerId { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
    
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public virtual Volunteer Volunteer { get; set; }
}