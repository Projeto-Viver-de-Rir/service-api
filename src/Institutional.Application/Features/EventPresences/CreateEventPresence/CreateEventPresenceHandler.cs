using Ardalis.Result;
using Institutional.Application.Common;
using Institutional.Application.Extensions;
using Institutional.Domain.Entities.Enums;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.EventPresences.CreateEventPresence;

public class CreateEventPresenceHandler : IRequestHandler<CreateEventPresenceRequest, Result<GetEventPresenceResponse>>
{
    private readonly IContext _context;
    
    
    public CreateEventPresenceHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetEventPresenceResponse>> Handle(CreateEventPresenceRequest request, CancellationToken cancellationToken)
    {
        var originalEvent = await _context.Events
            .FirstOrDefaultAsync(x => x.Id == request.EventId, cancellationToken);
        
        if (originalEvent == null) 
            return Result.NotFound();

        var volunteerDebts = _context.Debts.Count(x => x.VolunteerId == request.VolunteerId && !x.PaidAt.HasValue && x.DueDate <= request.AuditFields.StartedAt);

        if (volunteerDebts > 3)
            return Result.Error();

        var alreadyParticipating = _context.EventPresences
            .Any(x => x.EventId == request.EventId && x.VolunteerId == request.VolunteerId);
        
        if (alreadyParticipating)
            return Result.Error();
        
        var actualPresences = _context.EventPresences
            .Count(x => x.EventId == request.EventId);

        var configMultipleEvents = await _context.Configs
            .FirstOrDefaultAsync(x => x.Type == ConfigType.AllowMoreEventsSameDay, cancellationToken);
        bool allowMultipleVisits = configMultipleEvents != null && Convert.ToBoolean(configMultipleEvents.Value);

        if (!allowMultipleVisits)
        {
            var date = DateOnly.FromDateTime(originalEvent.HappenAt.Value);
            
            var alreadySignedForEvent =
                _context
                    .Events
                    .Where(p =>
                        p.HappenAt >= new DateTime(date.Year, date.Month, date.Day, 0, 0, 0) &&
                        p.HappenAt <= new DateTime(date.Year, date.Month, date.Day, 23, 59, 59))
                    .Include(p => p.Presences.Where(c => c.VolunteerId == request.VolunteerId))
                    .Any();

            if (alreadySignedForEvent)
                return Result.Error();
        }
        
        if (actualPresences < originalEvent.Occupancy)
        {
            var created = request.Adapt<Domain.Entities.EventPresence>();
            created.RegistrationAt = request.AuditFields!.StartedAt;
            created.CreatedBy = request.AuditFields!.StartedBy;
            created.CreatedAt = request.AuditFields!.StartedAt;

            _context.EventPresences.Add(created);
            await _context.SaveChangesAsync(cancellationToken);
            return created.Adapt<GetEventPresenceResponse>();    
        }
        
        return Result.Error();
    }
}