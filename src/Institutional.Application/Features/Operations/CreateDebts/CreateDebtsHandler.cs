using Ardalis.Result;
using Institutional.Application.Common;
using Institutional.Domain.Entities;
using Institutional.Domain.Entities.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Operations.CreateDebts;

public class CreateDebtsHandler : IRequestHandler<CreateDebtsRequest, Result<GetOperationsResponse>>
{
    private readonly IContext _context;
    
    
    public CreateDebtsHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetOperationsResponse>> Handle(CreateDebtsRequest request, CancellationToken cancellationToken)
    {
        var monthlyDebtAmount = await _context.Configs
            .FirstOrDefaultAsync(x => x.Type == ConfigType.MonthlyDebtAmount, cancellationToken);
        
        decimal amount = monthlyDebtAmount != null ? Convert.ToDecimal(monthlyDebtAmount.Value) : 10;
        var quantity = Math.Ceiling((request.EndsAt - request.StartsAt).Days / 30.0);

        List<Debt> debtsToGenerate = new();
        for (int occurence = 0; occurence < quantity; occurence++)
        {
            var expectedMonth = request.StartsAt.AddMonths(occurence);

            debtsToGenerate.Add(new()
                {
                    Name = string.Concat(expectedMonth.ToString("MM"), "/", expectedMonth.Year),
                    Description = "Monthly contribution",
                    Amount = amount,
                    DueDate = new DateTime(expectedMonth.Year, expectedMonth.Month, 15),
                    CreatedAt = request.AuditFields!.StartedAt,
                    CreatedBy = request.AuditFields!.StartedBy
                });
        }

        var volunteers = _context.Volunteers.Select(p => p.Id).ToImmutableList();
        var debts = volunteers.SelectMany(p => debtsToGenerate, (volunteer, debt) => new Debt()
        {
            Name = debt.Name,
            Description = debt.Description,
            Amount = debt.Amount,
            DueDate = debt.DueDate,
            VolunteerId = volunteer,
            CreatedAt = debt.CreatedAt,
            CreatedBy = debt.CreatedBy
        });

        _context.Debts.AddRange(debts);
        await _context.SaveChangesAsync(cancellationToken);
        return new GetOperationsResponse()
        {
            BaseItems = debtsToGenerate.Count(), 
            GeneratedItems = debts.Count()
        };
    }
}