using Ardalis.Result;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
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
        created.CreatedBy = request.AuditFields!.StartedBy;
        created.CreatedAt = request.AuditFields!.StartedAt;

        _context.ScheduleEvents.Add(created);

        if (request.Coordinators != null)
        {
            foreach (var item in request.Coordinators)
            {
                var scheduleEventCoordinator = new ScheduleEventCoordinator()
                {
                    ScheduleEventId = created.Id,
                    VolunteerId = item,
                    CreatedBy = request.AuditFields!.StartedBy,
                    CreatedAt = request.AuditFields!.StartedAt
                };

                _context.ScheduleEventCoordinators.Add(scheduleEventCoordinator);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        
        var response = created.Adapt<GetScheduleEventResponse>();
        //response.Coordinators = request.Coordinators;
        
        return response;
    }
}