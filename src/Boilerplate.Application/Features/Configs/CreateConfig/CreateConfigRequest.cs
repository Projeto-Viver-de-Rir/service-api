using Ardalis.Result;
using MediatR;

namespace Boilerplate.Application.Features.Configs.CreateConfig;

public record CreateConfigRequest : IRequest<Result<GetConfigResponse>>
{
    public string Key { get; init; } = null!;
    public string? Description { get; init; }
    public string Value { get; init; } = null!;
}