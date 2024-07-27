using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Accounts.GetMyselfV2;

public record GetMyselfV2Request(UserId Id) : IRequest<Result<GetMyselfResponseV2>>;