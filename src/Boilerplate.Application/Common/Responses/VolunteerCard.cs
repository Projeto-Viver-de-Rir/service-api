using Boilerplate.Domain.Entities.Common;

namespace Boilerplate.Application.Common.Responses;

public record VolunteerCard
{
    public VolunteerId Id { get; set; }
    public string Name { get; set; }
    public string Photo { get; set; }
}