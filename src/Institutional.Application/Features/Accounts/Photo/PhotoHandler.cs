using Ardalis.Result;
using Institutional.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Accounts.Photo;

public class PhotoHandler : IRequestHandler<PhotoRequest, Result<string>>
{
    private readonly IContext _context;
    private readonly IStorageService _storageService;
    
    public PhotoHandler(IContext context, IStorageService storageService)
    {
        _context = context;
        _storageService = storageService;
    }

    public async Task<Result<string>> Handle(PhotoRequest request, CancellationToken cancellationToken)
    {
        var name = $"{request.AuditFields!.StartedBy}/{Guid.NewGuid()}{request.FileExtension}";
        var userBucket = $"institutional-app";    
        
        var result = await _storageService.UploadFileAsync(userBucket, name, request.Input);
        
        var originalVolunteer = await _context.Volunteers
            .FirstOrDefaultAsync(x => x.AccountId == request.AuditFields!.StartedBy, cancellationToken);

        if (result.StatusCode == 201)
        {
            if (originalVolunteer == null)
                return name;
            
            originalVolunteer.Photo = name;
            _context.Volunteers.Update(originalVolunteer);
                
            await _context.SaveChangesAsync(cancellationToken);
                
            return name;
        }
        
        return string.Empty;
    }
}