using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.EventPresences.CreateEventPresence;

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
        
        var actualPresences = _context.EventPresences
            .Count(x => x.EventId == request.EventId);

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