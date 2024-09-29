using Ardalis.Result;
using Institutional.Application.Common;
using Institutional.Domain.Entities;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler : IRequestHandler<CreateVolunteerRequest, Result<GetVolunteerResponse>>
{
    private readonly IContext _context;
    
    
    public CreateVolunteerHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetVolunteerResponse>> Handle(CreateVolunteerRequest request, CancellationToken cancellationToken)
    {
        var created = request.Adapt<Volunteer>();
        
        created.Availability = created.Availability?.Replace(";", ",");
        created.CreatedBy = request.AuditFields!.StartedBy;
        created.CreatedAt = request.AuditFields!.StartedAt;
        
        _context.Volunteers.Add(created);
        await _context.SaveChangesAsync(cancellationToken);
        return created.Adapt<GetVolunteerResponse>();
    }
}