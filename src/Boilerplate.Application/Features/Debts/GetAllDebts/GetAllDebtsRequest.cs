using Boilerplate.Application.Common.Responses;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;

namespace Boilerplate.Application.Features.Debts.GetAllDebts;

public record GetAllDebtsRequest
    (string? Name = null, VolunteerId? VolunteerId = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetDebtResponse>>;