﻿using Boilerplate.Application.Common.Responses;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;

namespace Boilerplate.Application.Features.EventPresences.GetAllEventPresences;

public record GetAllEventPresencesRequest
    (string? Name = null, EventId? EventId = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetEventPresenceResponse>>;