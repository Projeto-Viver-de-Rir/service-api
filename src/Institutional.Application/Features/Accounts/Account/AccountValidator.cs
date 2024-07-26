using FluentValidation;
using Institutional.Application.Common;

namespace Institutional.Application.Features.Accounts.Account;

public class AccountValidator : AbstractValidator<AccountRequest>
{
    public AccountValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);
        
        RuleFor(x => x.Phone)
            .NotEmpty();
    }
}