using Ardalis.Result;
using Institutional.Application.Common;
using Institutional.Domain.Entities;
using Institutional.Domain.Entities.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Teams.UpdateTeam;

public class UpdateTeamHandler : IRequestHandler<UpdateTeamRequest, Result<UpdateTeamResponse>>
{
    private readonly IContext _context;

    public UpdateTeamHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<UpdateTeamResponse>> Handle(UpdateTeamRequest request,
        CancellationToken cancellationToken)
    {
        var originalTeam = await _context.Teams
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (originalTeam == null) return Result.NotFound();

        originalTeam.Name = request.Name;
        originalTeam.Description = request.Description;
        originalTeam.Type = request.Type;
        originalTeam.Status = request.Status;
        originalTeam.UpdatedBy = request.AuditFields!.StartedBy;
        originalTeam.UpdatedAt = request.AuditFields!.StartedAt;
        
        _context.Teams.Update(originalTeam);
        
        var originalTeamMembers = await _context.TeamMembers
            .Where(p => p.TeamId == request.Id).Select(p => p.VolunteerId).ToListAsync(cancellationToken);

        var response = originalTeam.Adapt<UpdateTeamResponse>();
        
        if (request.Members != null)
        {
            var membersToInsert = request.Members.Except(originalTeamMembers);
            response.UsersToAddRole =_context.Volunteers.Where(p => membersToInsert.Any(x => x == p.Id)).Select(p => p.AccountId);
            
            foreach (var item in membersToInsert)
            {
                var member = new TeamMember()
                {
                    TeamId = request.Id,
                    VolunteerId = item,
                    CreatedBy = request.AuditFields!.StartedBy,
                    CreatedAt = request.AuditFields!.StartedAt
                };
                
                _context.TeamMembers.Add(member);
            }
        }
        
        if (request.Members != null)
        {
            var membersToRemove = originalTeamMembers.Except(request.Members);
            response.UsersToRemoveRole =_context.Volunteers.Where(p => membersToRemove.Any(x => x == p.Id)).Select(p => p.AccountId);

            foreach (var item in membersToRemove)
            {
                var member = await _context.TeamMembers
                    .Where(p => p.VolunteerId == item && p.TeamId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                
                _context.TeamMembers.Remove(member);
            }
        }
        
        await _context.SaveChangesAsync(cancellationToken);
        return response;
    }
}