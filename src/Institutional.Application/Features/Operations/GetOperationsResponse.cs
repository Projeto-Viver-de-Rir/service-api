using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using System;

namespace Institutional.Application.Features.Operations;

public record GetOperationsResponse
{
    public int BaseItems { get; init; }
    public int GeneratedItems { get; init; }
}