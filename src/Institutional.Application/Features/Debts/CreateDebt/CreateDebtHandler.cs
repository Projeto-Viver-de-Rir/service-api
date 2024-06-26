﻿using Ardalis.Result;
using Institutional.Application.Common;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Debts.CreateDebt;

public class CreateDebtHandler : IRequestHandler<CreateDebtRequest, Result<GetDebtResponse>>
{
    private readonly IContext _context;
    
    
    public CreateDebtHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetDebtResponse>> Handle(CreateDebtRequest request, CancellationToken cancellationToken)
    {
        var created = request.Adapt<Domain.Entities.Debt>();
        created.CreatedBy = request.AuditFields!.StartedBy;
        created.CreatedAt = request.AuditFields!.StartedAt;

        if (created.PaidAt.HasValue)
            created.PaidBy = request.AuditFields!.StartedBy;

        _context.Debts.Add(created);
        await _context.SaveChangesAsync(cancellationToken);
        return created.Adapt<GetDebtResponse>();
    }
}