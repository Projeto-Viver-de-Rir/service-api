using Boilerplate.Application.Common;
using FluentValidation;

namespace Boilerplate.Application.Features.Configs.UpdateConfig;

public class UpdateConfigValidator : AbstractValidator<UpdateConfigRequest>
{
    public UpdateConfigValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Key)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);

        RuleFor(x => x.Value)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);
    }
}