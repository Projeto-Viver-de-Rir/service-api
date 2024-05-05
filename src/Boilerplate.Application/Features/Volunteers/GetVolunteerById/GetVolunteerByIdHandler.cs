using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Volunteers.GetVolunteerById;

public class GetVolunteerByIdHandler : IRequestHandler<GetVolunteerByIdRequest, Result<GetVolunteerResponse>>
{
    private readonly IContext _context;


    public GetVolunteerByIdHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result<GetVolunteerResponse>> Handle(GetVolunteerByIdRequest request, CancellationToken cancellationToken)
    {
        var volunteer = await _context.Volunteers.FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken: cancellationToken);
        if (volunteer is null) return Result.NotFound();
        return volunteer.Adapt<GetVolunteerResponse>();
    }
}