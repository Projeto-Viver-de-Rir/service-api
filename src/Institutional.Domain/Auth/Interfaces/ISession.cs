using Institutional.Domain.Entities.Common;
using System;

namespace Institutional.Domain.Auth.Interfaces;

public interface ISession
{
    public UserId UserId { get; }

    public DateTime Now { get; }
}