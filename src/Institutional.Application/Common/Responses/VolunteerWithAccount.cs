using Institutional.Domain.Entities.Common;

namespace Institutional.Application.Common.Responses;

public record VolunteerWithAccount
{
    public VolunteerId Id { get; set; }
    public string Name { get; set; }
    public UserId AccountId { get; set; }
}