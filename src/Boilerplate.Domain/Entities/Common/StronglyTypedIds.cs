using StronglyTypedIds;
using System;

[assembly: StronglyTypedIdDefaults(Template.Guid, "guid-efcore")]

namespace Boilerplate.Domain.Entities.Common;


public interface IGuid {}

[StronglyTypedId]
public partial struct UserId : IGuid
{
    public static implicit operator UserId(Guid guid)
    {
        return new UserId(guid);
    }
}

[StronglyTypedId]
public partial struct HeroId : IGuid
{
    public static implicit operator HeroId(Guid guid)
    {
        return new HeroId(guid);
    }
}

[StronglyTypedId]
public partial struct VolunteerId : IGuid
{
    public static implicit operator VolunteerId(Guid guid)
    {
        return new VolunteerId(guid);
    }
}

[StronglyTypedId]
public partial struct DebtId : IGuid
{
    public static implicit operator DebtId(Guid guid)
    {
        return new DebtId(guid);
    }
}

[StronglyTypedId]
public partial struct EventId : IGuid
{
    public static implicit operator EventId(Guid guid)
    {
        return new EventId(guid);
    }
}

[StronglyTypedId]
public partial struct TeamId : IGuid
{
    public static implicit operator TeamId(Guid guid)
    {
        return new TeamId(guid);
    }
}

[StronglyTypedId]
public partial struct ConfigurationId : IGuid
{
    public static implicit operator ConfigurationId(Guid guid)
    {
        return new ConfigurationId(guid);
    }
}