﻿using Institutional.Application.Common;
using FluentValidation;

namespace Institutional.Application.Features.Configs.UpdateConfig;

public class UpdateConfigValidator : AbstractValidator<UpdateConfigRequest>
{
    public UpdateConfigValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Type)
            .IsInEnum();

        RuleFor(x => x.Value)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);
    }
}