using Institutional.Application.Common.Responses;
using Institutional.Domain.Entities.Common;
using Institutional.Domain.Entities.Enums;
using MediatR;

namespace Institutional.Application.Features.Debts.GetAllDebts;

public record GetAllDebtsRequest
    (string? Name = null, VolunteerId? VolunteerId = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetDebtResponse>>;