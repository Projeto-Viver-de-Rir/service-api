﻿using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Boilerplate.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<Volunteer> Volunteers { get; set; } = null!;
    public DbSet<Debt> Debts { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<EventPresence> EventPresences { get; set; } = null!;
    public DbSet<ScheduleEvent> ScheduleEvents { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Config> Configs { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(VolunteerConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(DebtConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(EventConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(EventPresence).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(ScheduleEventConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(TeamConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(ConfigConfiguration).Assembly);
    }
}