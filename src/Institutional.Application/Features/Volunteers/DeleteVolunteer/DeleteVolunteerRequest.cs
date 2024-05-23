using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Volunteers.DeleteVolunteer;

public record DeleteVolunteerRequest(VolunteerId Id) : IRequest<Result>;