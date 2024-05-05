using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler : IRequestHandler<CreateVolunteerRequest, Result<GetVolunteerResponse>>
{
    private readonly IContext _context;
    
    
    public CreateVolunteerHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetVolunteerResponse>> Handle(CreateVolunteerRequest request, CancellationToken cancellationToken)
    {
        var created = request.Adapt<Domain.Entities.Volunteer>();
        _context.Volunteers.Add(created);
        await _context.SaveChangesAsync(cancellationToken);
        return created.Adapt<GetVolunteerResponse>();
    }
}