using Boilerplate.Application.Common.Responses;
using Boilerplate.Domain.Entities.Enums;
using MediatR;

namespace Boilerplate.Application.Features.Configs.GetAllConfig;

public record GetAllConfigsRequest
    (ConfigType? Type = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedList<GetConfigResponse>>;