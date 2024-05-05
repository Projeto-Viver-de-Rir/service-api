﻿using Boilerplate.Application.Common;
using FluentValidation;

namespace Boilerplate.Application.Features.Teams.UpdateTeam;

public class UpdateTeamValidator : AbstractValidator<UpdateTeamRequest>
{
    public UpdateTeamValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);

        RuleFor(x => x.Type)
            .IsInEnum();
        
        RuleFor(x => x.Status)
            .IsInEnum();
    }
}