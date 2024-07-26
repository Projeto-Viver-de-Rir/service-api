﻿using Ardalis.Result;
using Institutional.Application.Common;
using Institutional.Domain.Entities.Enums;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Accounts.EnrollAccount;

public class EnrollAccountHandler : IRequestHandler<EnrollAccountRequest, Result<GetMyselfResponse>>
{
    private readonly IContext _context;
    
    
    public EnrollAccountHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetMyselfResponse>> Handle(EnrollAccountRequest request, CancellationToken cancellationToken)
    {
        var allowNewVolunteers = await _context.Configs
            .FirstOrDefaultAsync(x => x.Type == ConfigType.RegistrationPeriodForNewVolunteers, cancellationToken);
        
        if (allowNewVolunteers == null) 
            return Result.NotFound();
        
        var registrationPeriod = JsonDocument.Parse(allowNewVolunteers.Value).RootElement;
        
        if (request.AuditFields!.StartedAt >= registrationPeriod.GetProperty("allowAt").GetDateTimeOffset() && 
            request.AuditFields!.StartedAt <= registrationPeriod.GetProperty("blockAfter").GetDateTimeOffset())
        {
            var created = request.Adapt<Domain.Entities.Volunteer>();
            
            created.AccountId = request.AuditFields!.StartedBy;
            created.CreatedBy = request.AuditFields!.StartedBy;
            created.CreatedAt = request.AuditFields!.StartedAt;

            _context.Volunteers.Add(created);
            await _context.SaveChangesAsync(cancellationToken);
            return created.Adapt<GetMyselfResponse>();   
        }
        
        return Result.Error();
    }
}