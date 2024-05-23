using Institutional.Application.Common;
using System;
using System.Collections.Generic;
using Institutional.Domain.Entities;
using Institutional.Domain.Entities.Enums;

namespace Institutional.Api.IntegrationTests.Helpers;

public static class TestingDatabase
{
    public static async Task SeedDatabase(Func<IContext> contextFactory)
    {
        await using var db = contextFactory();
        db.Volunteers.AddRange(GetSeedingVolunteers);
        await db.SaveChangesAsync();
    }


    public static readonly List<Volunteer> GetSeedingVolunteers =
        new()
        {
            new(){ Id = new Guid("824a7a65-b769-4b70-bccb-91f880b6ddf1"), Name = "Corban Best"},
            new() { Id = new Guid("b426070e-ccb3-42e6-8fb4-ef6aa5a62cc4"), Name = "Priya Hull"},
            new() { Id = new Guid("634769f7-a7b8-4146-9cb2-ff2dd90e886b"), Name = "Harrison Vu"}
        };
}