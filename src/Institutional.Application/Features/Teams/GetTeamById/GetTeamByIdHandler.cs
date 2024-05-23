using Ardalis.Result;
using Institutional.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Teams.GetTeamById;

public class GetTeamByIdHandler : IRequestHandler<GetTeamByIdRequest, Result<GetTeamResponse>>
{
    private readonly IContext _context;


    public GetTeamByIdHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result<GetTeamResponse>> Handle(GetTeamByIdRequest request, CancellationToken cancellationToken)
    {
        var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken: cancellationToken);
        if (team is null) return Result.NotFound();
        return team.Adapt<GetTeamResponse>();
    }
}