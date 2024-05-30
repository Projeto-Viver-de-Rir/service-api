using Institutional.Application.Common;
using Institutional.Application.Common.Responses;
using Institutional.Application.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Teams.GetAllTeams;

public class GetAllTeamsHandler : IRequestHandler<GetAllTeamsRequest, PaginatedList<GetTeamResponse>>
{
    private readonly IContext _context;
    
    public GetAllTeamsHandler(IContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<GetTeamResponse>> Handle(GetAllTeamsRequest request, CancellationToken cancellationToken)
    {
        var teams = _context.Teams
            .Include(p => p.Members)
            .WhereIf(!string.IsNullOrEmpty(request.Name), x => EF.Functions.Like(x.Name, $"%{request.Name}%"))
            .WhereIf(request.TeamType != null, x => x.Type == request.TeamType);
        return await teams.ProjectToType<GetTeamResponse>()
            .OrderBy(x => x.Name)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}