using FluentValidation;

namespace Boilerplate.Application.Features.Configs.DeleteConfig;

public class DeleteConfigValidator : AbstractValidator<DeleteConfigRequest>
{

    public DeleteConfigValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}