using Institutional.Domain.Entities.Common;
using MassTransit;
using System;

namespace Institutional.Domain.Entities;

public class Debt : Entity<DebtId>
{
    public override DebtId Id { get; set; } = NewId.NextGuid();
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public VolunteerId VolunteerId { get; set; }
    public DateTime? PaidAt { get; set; }
    public UserId? PaidBy { get; set; }
    
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserId? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public virtual Volunteer Volunteer { get; set; }
}