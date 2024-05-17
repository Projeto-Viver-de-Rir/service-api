using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;

namespace Boilerplate.Application.Features.Operations;

public record GetOperationsResponse
{
    public int BaseItems { get; init; }
    public int GeneratedItems { get; init; }
}