using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Accounts.GetMyself;

public record GetMyselfRequest(UserId Id) : IRequest<Result<GetMyselfResponse>>;