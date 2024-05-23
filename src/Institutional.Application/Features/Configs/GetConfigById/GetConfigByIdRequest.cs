using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Configs.GetConfigById;

public record GetConfigByIdRequest(ConfigId Id) : IRequest<Result<GetConfigResponse>>;