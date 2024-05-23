using Institutional.Application.Common.Responses;
using Institutional.Domain.Entities.Enums;
using MediatR;

namespace Institutional.Application.Features.Configs.GetAllConfig;

public record GetAllConfigsRequest
    (ConfigType? Type = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetConfigResponse>>;