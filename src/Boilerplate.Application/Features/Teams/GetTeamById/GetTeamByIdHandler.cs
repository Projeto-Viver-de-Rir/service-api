using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Teams.GetTeamById;

public class GetTeamByIdHandler : IRequestHandler<GetTeamByIdRequest, Result<GetTeamResponse>>
{
    private readonly IContext _context;


    public GetTeamByIdHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result<GetTeamResponse>> Handle(GetTeamByIdRequest request, CancellationToken cancellationToken)
    {
        var volunteer = await _context.Teams.FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken: cancellationToken);
        if (volunteer is null) return Result.NotFound();
        return volunteer.Adapt<GetTeamResponse>();
    }
}