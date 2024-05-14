using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.EventPresences.UpdateEventPresence;

public class UpdateEventPresenceHandler : IRequestHandler<UpdateEventPresenceRequest, Result<GetEventPresenceResponse>>
{
    private readonly IContext _context;

    public UpdateEventPresenceHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetEventPresenceResponse>> Handle(UpdateEventPresenceRequest request,
        CancellationToken cancellationToken)
    {
        var originalEventPresence = await _context.EventPresences
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (originalEventPresence == null) 
            return Result.NotFound();

        originalEventPresence.EventId = request.EventId;
        originalEventPresence.VolunteerId = request.VolunteerId;
        originalEventPresence.Attended = request.Attended;
        originalEventPresence.UpdatedBy = request.AuditFields!.StartedBy;
        originalEventPresence.UpdatedAt = request.AuditFields!.StartedAt;

        _context.EventPresences.Update(originalEventPresence);
        await _context.SaveChangesAsync(cancellationToken);
        return originalEventPresence.Adapt<GetEventPresenceResponse>();
    }
}