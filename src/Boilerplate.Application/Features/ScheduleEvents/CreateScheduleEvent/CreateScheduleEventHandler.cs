using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.ScheduleEvents.CreateScheduleEvent;

public class CreateScheduleEventHandler : IRequestHandler<CreateScheduleEventRequest, Result<GetScheduleEventResponse>>
{
    private readonly IContext _context;
    
    
    public CreateScheduleEventHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetScheduleEventResponse>> Handle(CreateScheduleEventRequest request, CancellationToken cancellationToken)
    {
        var created = request.Adapt<Domain.Entities.ScheduleEvent>();
        _context.ScheduleEvents.Add(created);
        await _context.SaveChangesAsync(cancellationToken);
        return created.Adapt<GetScheduleEventResponse>();
    }
}