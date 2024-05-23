using Ardalis.Result;
using Institutional.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Accounts.GetMyself;

public class GetMyselfHandler : IRequestHandler<GetMyselfRequest, Result<GetMyselfResponse>>
{
    private readonly IContext _context;


    public GetMyselfHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result<GetMyselfResponse>> Handle(GetMyselfRequest request, CancellationToken cancellationToken)
    {
        var volunteer = await _context.Volunteers.FirstOrDefaultAsync(x => x.AccountId == request.Id,
            cancellationToken: cancellationToken);

        if (volunteer is null) 
            return Result.NotFound();

        return volunteer.Adapt<GetMyselfResponse>();
    }
}