using Ardalis.Result;
using Institutional.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Volunteers.DeleteVolunteer;

public class DeleteVolunteerHandler : IRequestHandler<DeleteVolunteerRequest, Result>
{
    private readonly IContext _context;
    public DeleteVolunteerHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result> Handle(DeleteVolunteerRequest request, CancellationToken cancellationToken)
    {
        var volunteer = await _context.Volunteers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (volunteer is null) return Result.NotFound();
        _context.Volunteers.Remove(volunteer);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}