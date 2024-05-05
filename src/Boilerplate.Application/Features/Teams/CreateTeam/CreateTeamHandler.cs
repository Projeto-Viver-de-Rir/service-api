using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Teams.CreateTeam;

public class CreateTeamHandler : IRequestHandler<CreateTeamRequest, Result<GetTeamResponse>>
{
    private readonly IContext _context;
    
    
    public CreateTeamHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetTeamResponse>> Handle(CreateTeamRequest request, CancellationToken cancellationToken)
    {
        var created = request.Adapt<Domain.Entities.Team>();
        _context.Teams.Add(created);
        await _context.SaveChangesAsync(cancellationToken);
        return created.Adapt<GetTeamResponse>();
    }
}