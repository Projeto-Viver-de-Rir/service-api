using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Volunteers.GetAllVolunteers;

public class GetAllVolunteersHandler : IRequestHandler<GetAllVolunteersRequest, PaginatedList<GetVolunteerResponse>>
{
    private readonly IContext _context;
    
    public GetAllVolunteersHandler(IContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<GetVolunteerResponse>> Handle(GetAllVolunteersRequest request, CancellationToken cancellationToken)
    {
        var heroes = _context.Heroes
            .WhereIf(!string.IsNullOrEmpty(request.Name), x => EF.Functions.Like(x.Name, $"%{request.Name}%"))
            .WhereIf(!string.IsNullOrEmpty(request.Nickname), x => EF.Functions.Like(x.Nickname!, $"%{request.Nickname}%"));
        return await heroes.ProjectToType<GetVolunteerResponse>()
            .OrderBy(x => x.Name)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}