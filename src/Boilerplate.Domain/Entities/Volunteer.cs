using Boilerplate.Domain.Entities.Common;
using MassTransit;
using System;

namespace Boilerplate.Domain.Entities;

public class Volunteer : Entity<VolunteerId>
{
    public override VolunteerId Id { get; set; } = NewId.NextGuid();
    public string Name { get; set; } = null!;
    public string? Nickname { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Zip { get; set; }
    public string? Country { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Availability { get; set; }
    public string? Comments { get; set; }
    public string? Identifier { get; set; }
    public UserId AccountId { get; set; }
    public string? Photo { get; set; }
    
    public UserId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserId? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}