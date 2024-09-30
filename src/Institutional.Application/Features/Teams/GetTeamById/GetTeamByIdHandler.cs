using Ardalis.Result;
using Institutional.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Teams.GetTeamById;

public class GetTeamByIdHandler : IRequestHandler<GetTeamByIdRequest, Result<GetTeamResponse>>
{
    private readonly IContext _context;
    private readonly IStorageService _storageService;

    public GetTeamByIdHandler(IContext context, IStorageService storageService)
    {
        _context = context;
        _storageService = storageService;
    }
    public async Task<Result<GetTeamResponse>> Handle(GetTeamByIdRequest request, CancellationToken cancellationToken)
    {
        var team = await _context.Teams
            .Include(p => p.Members)
            .FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken: cancellationToken);
        
        if (team is null) 
            return Result.NotFound();

        var teamResponse = team.Adapt<GetTeamResponse>();

        foreach (var member in teamResponse.Members)
        {
            if (!string.IsNullOrWhiteSpace(member.Volunteer.Photo))
                member.Volunteer.Photo = await _storageService.GetFilePathAsync(member.Volunteer.Photo);    
        }
        
        return teamResponse;
    }
}