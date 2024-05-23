using Ardalis.Result;
using Institutional.Application.Common.Requests;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Events.ConclusionEvent;

public record ConclusionEventRequest : IRequest<Result<GetEventResponse>>
{
    [JsonIgnore]
    public EventId Id { get; init; }
    
    public IEnumerable<Guid> Presences { get; init; }
    
    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}