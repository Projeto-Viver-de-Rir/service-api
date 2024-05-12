using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
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
        var created = request.Adapt<Domain.Entities.EventPresence>();
        _context.EventPresences.Add(created);
        await _context.SaveChangesAsync(cancellationToken);
        return created.Adapt<GetEventPresenceResponse>();
    }
}