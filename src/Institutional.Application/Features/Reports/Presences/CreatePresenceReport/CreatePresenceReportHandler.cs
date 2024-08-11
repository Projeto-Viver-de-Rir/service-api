using Ardalis.Result;
using Institutional.Application.Common;
using Institutional.Domain.Entities;
using Institutional.Domain.Entities.Enums;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Reports.Presences.CreatePresenceReport;

public class CreatePresenceReportHandler : IRequestHandler<CreatePresenceReportRequest, Result<CreateReportsResponse>>
{
    private readonly IContext _context;
    
    
    public CreatePresenceReportHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<CreateReportsResponse>> Handle(CreatePresenceReportRequest request, CancellationToken cancellationToken)
    {
        if (request.AuditFields!.StartedAt.Day > 10)
            return Result.Error("Este relatório só pode ser gerado até o dia 10 de cada mês.");

        var dayLimit = new DateTime(request.AuditFields!.StartedAt.Year, request.AuditFields!.StartedAt.Month, 1, 0, 0, 0, kind: DateTimeKind.Utc);
        var firstDayPreviousMonth = dayLimit.AddMonths(-2);
        var firstDayLastMonth = dayLimit.AddMonths(-1);
        
        _context.ReportPresences.RemoveRange(_context.ReportPresences);
        
        var presences = _context.EventPresences
            .Include(p => p.Event)
            .Where(p => p.Attended && p.Event.Status == EventStatus.Realized && p.Event.HappenAt >= firstDayPreviousMonth && p.Event.HappenAt < dayLimit)
            .GroupBy(g => g.VolunteerId)
            .Select(p => new ReportPresence()
            {
                VolunteerId = p.Key,
                PreviousMonthAttendance = p.Count(x => x.Event.HappenAt >= firstDayPreviousMonth && x.Event.HappenAt < firstDayLastMonth),
                LastMonthAttendance = p.Count(x => x.Event.HappenAt >= firstDayLastMonth && x.Event.HappenAt < dayLimit),
                CreatedAt = request.AuditFields!.StartedAt,
                CreatedBy = request.AuditFields!.StartedBy
            });
        
        await _context.ReportPresences.AddRangeAsync(presences, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return new CreateReportsResponse()
        {
            GeneratedItems = presences.Count()
        };
    }
}