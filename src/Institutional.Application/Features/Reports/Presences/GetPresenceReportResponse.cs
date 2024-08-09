using Institutional.Application.Common.Responses;
using Institutional.Domain.Entities.Common;
using System;

namespace Institutional.Application.Features.Reports.Presences;

public record GetPresenceReportResponse
{
    public ReportPresenceId Id { get; init; }
    public VolunteerId VolunteerId { get; init; }
    
    public int PreviousMonthAttendance { get; set; }
    public int LastMonthAttendance { get; set; }
    
    public VolunteerCard Volunteer { get; set; }
    
    public DateTime CreatedAt { get; set; }
}