using Ardalis.Result;
using Institutional.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Accounts.GetMyselfV2;

public class GetMyselfV2Handler : IRequestHandler<GetMyselfV2Request, Result<GetMyselfResponseV2>>
{
    private readonly IContext _context;
    private readonly IStorageService _storageService;

    public GetMyselfV2Handler(IContext context, IStorageService storageService)
    {
        _context = context;
        _storageService = storageService;
    }
    public async Task<Result<GetMyselfResponseV2>> Handle(GetMyselfV2Request request, CancellationToken cancellationToken)
    {
        var result = new GetMyselfResponseV2 { Id = request.Id, Email = request.Email, Phone = request.Phone};
        
        var volunteer = await _context.Volunteers.FirstOrDefaultAsync(x => x.AccountId == request.Id,
            cancellationToken: cancellationToken);

        if (volunteer is not null)
        {
            result.Volunteer = volunteer.Adapt<VolunteerInformation>();
            
            if (!string.IsNullOrWhiteSpace(result.Volunteer.Photo))
                result.Volunteer.Photo = await _storageService.GetFilePathAsync(result.Volunteer.Photo);

            var teamIds = _context.TeamMembers
                .Where(p => p.VolunteerId == volunteer.Id)
                .Select(p => p.TeamId);

            var roles = _context.Teams
                .Where(p => teamIds.Contains(p.Id))
                .Select(p => p.Type.ToString().ToLowerInvariant());

            if (roles.Any())
                result.Permissions = result.Permissions!.Concat(roles);
        }
        
        return result;
    }
}