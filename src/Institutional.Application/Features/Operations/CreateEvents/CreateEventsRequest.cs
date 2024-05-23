using Ardalis.Result;
using Institutional.Application.Common.Requests;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Operations.CreateEvents;

public record CreateEventsRequest : IRequest<Result<GetOperationsResponse>>
{
    public DateTime MonthToGenerate { get; init; }

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}