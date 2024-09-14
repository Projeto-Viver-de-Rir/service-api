using Ardalis.Result;
using Institutional.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Events.GetEventById;

public class GetEventByIdHandler : IRequestHandler<GetEventByIdRequest, Result<GetEventResponse>>
{
    private readonly IContext _context;


    public GetEventByIdHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result<GetEventResponse>> Handle(GetEventByIdRequest request, CancellationToken cancellationToken)
    {
        var eventItem = await _context.Events
            .Include(p => p.Coordinators)
            .Include(p => p.Presences)
            .FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken: cancellationToken);
        
        if (eventItem is null) 
            return Result.NotFound();

        var result = eventItem.Adapt<GetEventResponse>();
        result.Capacity = eventItem.Occupancy - eventItem.Presences.Count;
        
        return result;
    }
}