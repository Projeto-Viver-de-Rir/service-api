using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.EventPresences.GetEventPresenceById;

public class GetEventPresenceByIdHandler : IRequestHandler<GetEventPresenceByIdRequest, Result<GetEventPresenceResponse>>
{
    private readonly IContext _context;


    public GetEventPresenceByIdHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result<GetEventPresenceResponse>> Handle(GetEventPresenceByIdRequest request, CancellationToken cancellationToken)
    {
        var eventPresence = await _context.EventPresences.Include(p => p.Volunteer)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        
        if (eventPresence is null) 
            return Result.NotFound();
        
        return eventPresence.Adapt<GetEventPresenceResponse>();
    }
}