using Ardalis.Result;
using Institutional.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Events.DeleteEvent;

public class DeleteEventHandler : IRequestHandler<DeleteEventRequest, Result>
{
    private readonly IContext _context;
    public DeleteEventHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result> Handle(DeleteEventRequest request, CancellationToken cancellationToken)
    {
        var eventItem = await _context.Events.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (eventItem is null) return Result.NotFound();
        _context.Events.Remove(eventItem);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}