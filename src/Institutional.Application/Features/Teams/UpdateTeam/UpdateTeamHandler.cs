﻿using Ardalis.Result;
using Institutional.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Teams.UpdateTeam;

public class UpdateTeamHandler : IRequestHandler<UpdateTeamRequest, Result<GetTeamResponse>>
{
    private readonly IContext _context;

    public UpdateTeamHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetTeamResponse>> Handle(UpdateTeamRequest request,
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
        await _context.SaveChangesAsync(cancellationToken);
        return originalTeam.Adapt<GetTeamResponse>();
    }
}