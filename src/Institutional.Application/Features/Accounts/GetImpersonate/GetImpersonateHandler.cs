using Ardalis.Result;
using Institutional.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Accounts.GetImpersonate;

public class GetImpersonateHandler : IRequestHandler<GetImpersonateRequest, Result<GetMyselfResponse>>
{
    private readonly IContext _context;


    public GetImpersonateHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result<GetMyselfResponse>> Handle(GetImpersonateRequest request, CancellationToken cancellationToken)
    {
        var volunteer = await _context.Volunteers.FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken: cancellationToken);

        if (volunteer is null) 
            return Result.NotFound();
        
        return volunteer.Adapt<GetMyselfResponse>();
    }
}