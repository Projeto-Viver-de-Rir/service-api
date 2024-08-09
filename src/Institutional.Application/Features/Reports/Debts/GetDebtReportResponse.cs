using Institutional.Application.Common.Responses;
using Institutional.Domain.Entities.Common;
using System;

namespace Institutional.Application.Features.Reports.Debts;

public record GetDebtReportResponse
{
    public ReportDebtId Id { get; init; }
    public VolunteerId VolunteerId { get; init; }
    
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
    
    public VolunteerCard Volunteer { get; set; }
    
    public DateTime CreatedAt { get; set; }
}