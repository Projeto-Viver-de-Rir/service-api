﻿using Ardalis.Result;
using Institutional.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Debts.GetDebtById;

public class GetDebtByIdHandler : IRequestHandler<GetDebtByIdRequest, Result<GetDebtResponse>>
{
    private readonly IContext _context;


    public GetDebtByIdHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result<GetDebtResponse>> Handle(GetDebtByIdRequest request, CancellationToken cancellationToken)
    {
        var debt = await _context.Debts
            .Include(p => p.Volunteer)
            .FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken: cancellationToken);
        
        if (debt is null) 
            return Result.NotFound();
        
        return debt.Adapt<GetDebtResponse>();
    }
}