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
    private readonly IStorageService _storageService;

    public GetEventByIdHandler(IContext context, IStorageService storageService)
    {
        _context = context;
        _storageService = storageService;
    }
    public async Task<Result<GetEventResponse>> Handle(GetEventByIdRequest request, CancellationToken cancellationToken)
    {
        var eventItem = await _context.Events
            .Include(p => p.Coordinators)
                .ThenInclude(c => c.Volunteer)
            .Include(p => p.Presences)
                .ThenInclude(c => c.Volunteer)
            .FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken: cancellationToken);
        
        if (eventItem is null) 
            return Result.NotFound();

        var result = eventItem.Adapt<GetEventResponse>();
        result.Capacity = eventItem.Occupancy - eventItem.Presences.Count;
        
        foreach (var presence in result.Presences)
        {
            if (!string.IsNullOrWhiteSpace(presence.Volunteer.Photo))
                presence.Volunteer.Photo = await _storageService.GetFilePathAsync(presence.Volunteer.Photo);    
        }
        
        foreach (var coordinator in result.Coordinators)
        {
            if (!string.IsNullOrWhiteSpace(coordinator.Volunteer.Photo))
                coordinator.Volunteer.Photo = await _storageService.GetFilePathAsync(coordinator.Volunteer.Photo);    
        }
        
        return result;
    }
}