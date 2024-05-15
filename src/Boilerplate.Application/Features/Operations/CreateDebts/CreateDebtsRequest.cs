using Ardalis.Result;
using Boilerplate.Application.Common.Requests;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.Operations.CreateDebts;

public record CreateDebtsRequest : IRequest<Result<GetOperationsResponse>>
{
    public DateTime StartsAt { get; init; }
    public DateTime EndsAt { get; init; }

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}