using Ardalis.Result;
using Institutional.Application.Common;
using Institutional.Domain.Entities;
using Institutional.Domain.Entities.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Teams.CreateTeam;

public class CreateTeamHandler : IRequestHandler<CreateTeamRequest, Result<GetTeamResponse>>
{
    private readonly IContext _context;
    
    
    public CreateTeamHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetTeamResponse>> Handle(CreateTeamRequest request, CancellationToken cancellationToken)
    {
        var created = request.Adapt<Domain.Entities.Team>();
        created.CreatedBy = request.AuditFields!.StartedBy;
        created.CreatedAt = request.AuditFields!.StartedAt;
        
        created.Members.Clear();
        
        _context.Teams.Add(created);

        await _context.SaveChangesAsync(cancellationToken);
        
        if (request.Members != null)
        {
            foreach (var item in request.Members)
            {
                var teamMember = new TeamMember()
                {
                    TeamId = created.Id,
                    VolunteerId = item,
                    CreatedBy = request.AuditFields!.StartedBy,
                    CreatedAt = request.AuditFields!.StartedAt
                };

                _context.TeamMembers.Add(teamMember);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        
        var team = await _context.Teams
            .Include(p => p.Members)
            .SingleOrDefaultAsync(x => x.Id == created.Id, CancellationToken.None);
        
        if (team is null) 
            return Result.NotFound();
        
        return team.Adapt<GetTeamResponse>();
    }
}