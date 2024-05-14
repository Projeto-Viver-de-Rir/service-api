using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Events.CreateEvent;

public class CreateEventHandler : IRequestHandler<CreateEventRequest, Result<GetEventResponse>>
{
    private readonly IContext _context;
    
    
    public CreateEventHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetEventResponse>> Handle(CreateEventRequest request, CancellationToken cancellationToken)
    {
        var created = request.Adapt<Domain.Entities.Event>();
        created.CreatedBy = request.AuditFields!.StartedBy;
        created.CreatedAt = request.AuditFields!.StartedAt;

        _context.Events.Add(created);
        await _context.SaveChangesAsync(cancellationToken);
        return created.Adapt<GetEventResponse>();
    }
}