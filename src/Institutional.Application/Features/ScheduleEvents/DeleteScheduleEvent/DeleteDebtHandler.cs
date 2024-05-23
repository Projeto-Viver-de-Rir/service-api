using Ardalis.Result;
using Institutional.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.ScheduleEvents.DeleteScheduleEvent;

public class DeleteScheduleEventHandler : IRequestHandler<DeleteScheduleEventRequest, Result>
{
    private readonly IContext _context;
    public DeleteScheduleEventHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result> Handle(DeleteScheduleEventRequest request, CancellationToken cancellationToken)
    {
        var scheduleEvent = await _context.ScheduleEvents.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (scheduleEvent is null) return Result.NotFound();
        _context.ScheduleEvents.Remove(scheduleEvent);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}