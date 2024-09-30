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
    private readonly IStorageService _storageService;
    
    public GetAllTeamsHandler(IContext context, IStorageService storageService)
    {
        _context = context;
        _storageService = storageService;
    }
    public async Task<PaginatedList<GetTeamResponse>> Handle(GetAllTeamsRequest request, CancellationToken cancellationToken)
    {
        var teams = _context.Teams
            .Include(p => p.Members)
            .WhereIf(!string.IsNullOrEmpty(request.Name), x => EF.Functions.Like(x.Name, $"%{request.Name}%"))
            .WhereIf(request.TeamType != null, x => x.Type == request.TeamType);
        
        var paginatedListAsync = await teams.ProjectToType<GetTeamResponse>()
            .OrderBy(x => x.Name)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        foreach (var item in paginatedListAsync.Result)
            foreach (var member in item.Members)
                if (!string.IsNullOrWhiteSpace(member.Volunteer.Photo))
                    member.Volunteer.Photo = await _storageService.GetFilePathAsync(member.Volunteer.Photo);

        return paginatedListAsync;
    }
}