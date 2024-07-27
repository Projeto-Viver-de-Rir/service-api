using Ardalis.Result;
using Institutional.Application.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Features.Accounts.Account;

public class AccountHandler : IRequestHandler<AccountRequest, Result<string>>
{
    private readonly IContext _context;
    
    
    public AccountHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<string>> Handle(AccountRequest request, CancellationToken cancellationToken)
    {
        // TODO: Update AspNetUsers
        
        // var originalAccount = await _context.Users
        //     .FirstOrDefaultAsync(x => x.Id == request.AuditFields!.StartedBy, cancellationToken);
        // if (originalAccount == null) return Result.NotFound();
        //
        // originalAccount.Name = request.FullName;
        // originalAccount.Email = request.Email;
        // originalAccount.Phone = request.Phone;
        // originalAccount.UpdatedBy = request.AuditFields!.StartedBy;
        // originalAccount.UpdatedAt = request.AuditFields!.StartedAt;
        //
        // _context.Users.Update(originalAccount);
        // await _context.SaveChangesAsync(cancellationToken);
        // return originalAccount.Adapt<GetMyselfResponse>();

        return "Changed successfully.";
    }
}