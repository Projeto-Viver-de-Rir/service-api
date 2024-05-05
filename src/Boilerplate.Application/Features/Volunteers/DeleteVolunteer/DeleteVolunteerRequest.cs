using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Volunteers.DeleteVolunteer;

public record DeleteVolunteerRequest(VolunteerId Id) : IRequest<Result>;