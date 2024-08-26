using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Accounts.GetMyselfV2;

public record GetMyselfV2Request : IRequest<Result<GetMyselfResponseV2>>
{
    public UserId Id { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
}