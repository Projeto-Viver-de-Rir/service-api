using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Accounts.GetImpersonate;

public record GetImpersonateRequest(VolunteerId Id) : IRequest<Result<GetMyselfResponse>>;