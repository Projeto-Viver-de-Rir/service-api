using Institutional.Domain.Entities.Common;

namespace Institutional.Application.Common.Responses;

public record VolunteerCard
{
    public VolunteerId Id { get; set; }
    public string Name { get; set; }
    public string Nickname { get; set; }
    public string Photo { get; set; }
    public UserId AccountId { get; set; }
}