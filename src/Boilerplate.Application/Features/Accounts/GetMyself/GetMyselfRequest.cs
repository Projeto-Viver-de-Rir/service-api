using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Accounts.GetMyself;

public record GetMyselfRequest(UserId Id) : IRequest<Result<GetMyselfResponse>>;