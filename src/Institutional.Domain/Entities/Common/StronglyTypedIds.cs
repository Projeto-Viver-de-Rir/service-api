using StronglyTypedIds;
using System;

[assembly: StronglyTypedIdDefaults(Template.Guid, "guid-efcore")]

namespace Institutional.Domain.Entities.Common;


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
public partial struct ConfigId : IGuid
{
    public static implicit operator ConfigId(Guid guid)
    {
        return new ConfigId(guid);
    }
}

[StronglyTypedId]
public partial struct ScheduleEventId : IGuid
{
    public static implicit operator ScheduleEventId(Guid guid)
    {
        return new ScheduleEventId(guid);
    }
}

[StronglyTypedId]
public partial struct EventPresenceId : IGuid
{
    public static implicit operator EventPresenceId(Guid guid)
    {
        return new EventPresenceId(guid);
    }
}

[StronglyTypedId]
public partial struct EventCoordinatorId : IGuid
{
    public static implicit operator EventCoordinatorId(Guid guid)
    {
        return new EventCoordinatorId(guid);
    }
}

[StronglyTypedId]
public partial struct ScheduleEventCoordinatorId : IGuid
{
    public static implicit operator ScheduleEventCoordinatorId(Guid guid)
    {
        return new ScheduleEventCoordinatorId(guid);
    }
}