﻿using Ardalis.Result;
using Institutional.Application.Common.Requests;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Teams.CreateTeam;

public record CreateTeamRequest : IRequest<Result<GetTeamResponse>>
{
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public TeamType Type { get; init; }
    public TeamStatus Status { get; init; }
    public IEnumerable<VolunteerId>? Members { get; init; } = Enumerable.Empty<VolunteerId>();

    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}