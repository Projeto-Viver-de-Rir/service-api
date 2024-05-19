using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.EventPresences.GetAllEventPresences;

public class GetAllEventPresencesHandler : IRequestHandler<GetAllEventPresencesRequest, PaginatedList<GetEventPresenceResponse>>
{
    private readonly IContext _context;
    
    public GetAllEventPresencesHandler(IContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<GetEventPresenceResponse>> Handle(GetAllEventPresencesRequest request, CancellationToken cancellationToken)
    {
        var eventPresences = _context.EventPresences
            .Include(p => p.Volunteer);
        
        return await eventPresences.ProjectToType<GetEventPresenceResponse>()
            .WhereIf(request.EventId.HasValue, x => x.EventId == request.EventId)
            .OrderBy(x => x.RegistrationAt)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}