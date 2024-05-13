using Ardalis.Result;
using Boilerplate.Application.Common.Requests;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.Events.ConclusionEvent;

public record ConclusionEventRequest : IRequest<Result<GetEventResponse>>
{
    [JsonIgnore]
    public EventId Id { get; init; }
    
    public IEnumerable<Guid> Presences { get; init; }
    
    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}