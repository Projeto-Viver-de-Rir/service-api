using Institutional.Application.Common.Responses;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Debts.GetAllDebts;

public record GetAllDebtsRequest
    (bool? Paid, int? Year, string? Name = null, VolunteerId? VolunteerId = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetDebtResponse>>;