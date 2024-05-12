using Ardalis.Result;
using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.EventPresences.DeleteEventPresence;

public class DeleteEventPresenceHandler : IRequestHandler<DeleteEventPresenceRequest, Result>
{
    private readonly IContext _context;
    public DeleteEventPresenceHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result> Handle(DeleteEventPresenceRequest presenceRequest, CancellationToken cancellationToken)
    {
        var eventPresence = await _context.EventPresences.FirstOrDefaultAsync(x => x.Id == presenceRequest.Id, cancellationToken);
        if (eventPresence is null) return Result.NotFound();
        _context.EventPresences.Remove(eventPresence);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}