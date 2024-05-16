using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Accounts.GetImpersonate;

public record GetImpersonateRequest(VolunteerId Id) : IRequest<Result<GetMyselfResponse>>;