using Institutional.Domain.Entities.Common;
using System;

namespace Institutional.Application.Common.Requests;

public record AuditData
{
    public AuditData(string loggedUserId)
    {
        StartedBy = Guid.Parse(loggedUserId);
        StartedAt = DateTime.UtcNow;
    }
    
    public UserId StartedBy { get; init; }
    public DateTime StartedAt { get; init; }
}