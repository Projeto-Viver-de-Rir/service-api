using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Configs.DeleteConfig;

public record DeleteConfigRequest(ConfigId Id) : IRequest<Result>;