﻿using MassTransit;
using Microsoft.AspNetCore.Identity;
using System;

namespace Institutional.Infrastructure;

public class ApplicationUser : IdentityUser<Guid>
{
    public override Guid Id { get; set; } = NewId.NextSequentialGuid();
}

public class ApplicationRole : IdentityRole<Guid>
{
    public override Guid Id { get; set; } = NewId.NextSequentialGuid();
}