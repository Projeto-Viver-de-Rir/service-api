using Institutional.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using ISession = Institutional.Domain.Auth.Interfaces.ISession;

namespace Institutional.Application.Auth;

public class Session : ISession
{
    public UserId UserId { get; private init; }

    public DateTime Now => DateTime.Now;

    public Session(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        var nameIdentifier = user?.FindFirst(ClaimTypes.NameIdentifier);

        if(nameIdentifier != null)
        {
            UserId = new Guid(nameIdentifier.Value);
        }
    }

}