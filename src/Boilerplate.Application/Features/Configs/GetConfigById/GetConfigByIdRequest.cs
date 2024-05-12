using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Configs.GetConfigById;

public record GetConfigByIdRequest(ConfigId Id) : IRequest<Result<GetConfigResponse>>;