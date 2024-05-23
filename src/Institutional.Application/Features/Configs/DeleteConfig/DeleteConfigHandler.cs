using Ardalis.Result;
using Institutional.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Configs.DeleteConfig;

public class DeleteConfigHandler : IRequestHandler<DeleteConfigRequest, Result>
{
    private readonly IContext _context;
    public DeleteConfigHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result> Handle(DeleteConfigRequest request, CancellationToken cancellationToken)
    {
        var team = await _context.Configs.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (team is null) return Result.NotFound();
        _context.Configs.Remove(team);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}