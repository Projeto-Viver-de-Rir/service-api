using Ardalis.Result;
using Institutional.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Volunteers.GetVolunteerById;

public class GetVolunteerByIdHandler : IRequestHandler<GetVolunteerByIdRequest, Result<GetVolunteerResponse>>
{
    private readonly IContext _context;
    private readonly IStorageService _storageService;

    public GetVolunteerByIdHandler(IContext context, IStorageService storageService)
    {
        _context = context;
        _storageService = storageService;
    }
    public async Task<Result<GetVolunteerResponse>> Handle(GetVolunteerByIdRequest request, CancellationToken cancellationToken)
    {
        var volunteer = await _context.Volunteers.FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken: cancellationToken);
        
        if (volunteer is null) 
            return Result.NotFound();

        var result = volunteer.Adapt<GetVolunteerResponse>();
        if (!string.IsNullOrWhiteSpace(result.Photo))
            result.Photo = await _storageService.GetFilePathAsync(result.Photo);

        return result;
    }
}