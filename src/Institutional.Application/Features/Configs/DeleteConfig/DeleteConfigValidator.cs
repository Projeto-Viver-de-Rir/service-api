using FluentValidation;

namespace Institutional.Application.Features.Configs.DeleteConfig;

public class DeleteConfigValidator : AbstractValidator<DeleteConfigRequest>
{

    public DeleteConfigValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}