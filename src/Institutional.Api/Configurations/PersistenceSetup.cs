using Institutional.Application.Auth;
using Institutional.Domain.Auth.Interfaces;
using Institutional.Infrastructure;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Institutional.Api.Configurations;

public static class PersistenceSetup
{
    public static IServiceCollection AddPersistenceSetup(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<ISession, Session>();
        services.AddHostedService<ApplicationDbInitializer>();
        
        // Add a DbContext to store Domain data
        services.AddDbContextPool<ApplicationDbContext>(o =>
        {
            o.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            o.UseExceptionProcessor();
        });
        
        services.AddDataProtection()
            .PersistKeysToDbContext<ApplicationDbContext>();
        
        return services;
    }
}