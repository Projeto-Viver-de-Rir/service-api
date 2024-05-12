using Boilerplate.Application.Common;
using FluentValidation;

namespace Boilerplate.Application.Features.Configs.CreateConfig;

public class CreateConfigValidator : AbstractValidator<CreateConfigRequest>
{
    public CreateConfigValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        RuleFor(x => x.Key)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);

        RuleFor(x => x.Value)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);
    }
}