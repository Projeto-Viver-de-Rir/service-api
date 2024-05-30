using Ardalis.Result;
using Institutional.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Teams.DeleteTeam;

public class DeleteTeamHandler : IRequestHandler<DeleteTeamRequest, Result>
{
    private readonly IContext _context;
    public DeleteTeamHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result> Handle(DeleteTeamRequest request, CancellationToken cancellationToken)
    {
        var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (team is null) 
            return Result.NotFound();
        
        _context.Teams.Remove(team);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}