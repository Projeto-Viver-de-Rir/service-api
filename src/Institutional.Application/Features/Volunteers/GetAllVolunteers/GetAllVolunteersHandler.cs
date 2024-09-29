using Institutional.Application.Common;
using Institutional.Application.Common.Responses;
using Institutional.Application.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Volunteers.GetAllVolunteers;

public class GetAllVolunteersHandler : IRequestHandler<GetAllVolunteersRequest, PaginatedList<GetVolunteerResponse>>
{
    private readonly IContext _context;
    
    public GetAllVolunteersHandler(IContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<GetVolunteerResponse>> Handle(GetAllVolunteersRequest request, CancellationToken cancellationToken)
    {
        var volunteers = _context.Volunteers
            .WhereIf(!string.IsNullOrEmpty(request.Name), x => EF.Functions.Like(x.Name.ToLower(), $"%{request.Name!.ToLower()}%"))
            .WhereIf(!string.IsNullOrEmpty(request.Nickname), x => request.Nickname != null && EF.Functions.Like(x.Nickname!.ToLower(), $"%{request.Nickname.ToLower()}%"));
        return await volunteers.ProjectToType<GetVolunteerResponse>()
            .OrderBy(x => x.Name)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}