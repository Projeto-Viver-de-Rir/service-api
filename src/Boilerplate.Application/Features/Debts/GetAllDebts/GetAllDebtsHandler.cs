using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Debts.GetAllDebts;

public class GetAllDebtsHandler : IRequestHandler<GetAllDebtsRequest, PaginatedList<GetDebtResponse>>
{
    private readonly IContext _context;
    
    public GetAllDebtsHandler(IContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<GetDebtResponse>> Handle(GetAllDebtsRequest request, CancellationToken cancellationToken)
    {
        var debts = _context.Debts
            .WhereIf(!string.IsNullOrEmpty(request.Name), x => EF.Functions.Like(x.Name, $"%{request.Name}%"))
            .WhereIf(request.VolunteerId.HasValue, x => x.VolunteerId == request.VolunteerId);
        return await debts.ProjectToType<GetDebtResponse>()
            .OrderBy(x => x.Name)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}