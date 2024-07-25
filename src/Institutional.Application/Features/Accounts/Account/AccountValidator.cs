using Institutional.Application.Common;
using FluentValidation;

namespace Institutional.Application.Features.Accounts.Account;

public class AccountValidator : AbstractValidator<AccountRequest>
{
    public AccountValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);
        
        RuleFor(x => x.Phone)
            .NotEmpty();
    }
}