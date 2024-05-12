using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Configs.DeleteConfig;

public record DeleteConfigRequest(ConfigId Id) : IRequest<Result>;