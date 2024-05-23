using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Volunteers.GetVolunteerById;

public record GetVolunteerByIdRequest(VolunteerId Id) : IRequest<Result<GetVolunteerResponse>>;