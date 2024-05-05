﻿using Boilerplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Common;

public interface IContext : IAsyncDisposable, IDisposable
{
    public DatabaseFacade Database { get; }
    
    public DbSet<Hero> Heroes { get; }
    public DbSet<Volunteer> Volunteers { get; }
    public DbSet<Debt> Debts { get; }
    public DbSet<Event> Events { get; }
    public DbSet<Team> Teams { get; }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}