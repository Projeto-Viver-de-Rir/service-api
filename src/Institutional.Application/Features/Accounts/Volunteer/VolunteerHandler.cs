using Ardalis.Result;
using Institutional.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Accounts.Volunteer;

public class VolunteerHandler : IRequestHandler<VolunteerRequest, Result<GetMyselfResponse>>
{
    private readonly IContext _context;
    
    
    public VolunteerHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetMyselfResponse>> Handle(VolunteerRequest request, CancellationToken cancellationToken)
    {
        var originalVolunteer = await _context.Volunteers
            .FirstOrDefaultAsync(x => x.AccountId == request.AuditFields!.StartedBy, cancellationToken);
        
        if (originalVolunteer == null) 
            return Result.NotFound();

        originalVolunteer.Name = request.Name;
        originalVolunteer.Nickname = request.Nickname;
        originalVolunteer.Address = request.Address;
        originalVolunteer.City = request.City;
        originalVolunteer.State = request.State;
        originalVolunteer.Zip = request.Zip;
        originalVolunteer.Country = request.Country;
        originalVolunteer.BirthDate = request.BirthDate;
        originalVolunteer.Availability = request.Availability;
        originalVolunteer.Identifier = request.Identifier;
        originalVolunteer.UpdatedBy = request.AuditFields!.StartedBy;
        originalVolunteer.UpdatedAt = request.AuditFields!.StartedAt;
        
        _context.Volunteers.Update(originalVolunteer);
        await _context.SaveChangesAsync(cancellationToken);
        return originalVolunteer.Adapt<GetMyselfResponse>();
    }
}