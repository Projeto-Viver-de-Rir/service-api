using Institutional.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Institutional.Application.Common;

public interface IContext : IAsyncDisposable, IDisposable
{
    public DatabaseFacade Database { get; }
    
    public DbSet<Volunteer> Volunteers { get; }
    public DbSet<Debt> Debts { get; }
    public DbSet<Event> Events { get; }
    public DbSet<EventPresence> EventPresences { get; }
    public DbSet<EventCoordinator> EventCoordinators { get; }
    public DbSet<ScheduleEvent> ScheduleEvents { get; }
    public DbSet<ScheduleEventCoordinator> ScheduleEventCoordinators { get; }
    public DbSet<Team> Teams { get; }
    public DbSet<TeamMember> TeamMembers { get; }
    public DbSet<Config> Configs { get; }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}