using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Volunteers.GetVolunteerById;

public record GetVolunteerByIdRequest(VolunteerId Id) : IRequest<Result<GetVolunteerResponse>>;