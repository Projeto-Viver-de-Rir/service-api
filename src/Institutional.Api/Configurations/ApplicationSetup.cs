using Institutional.Application.Common;
using Institutional.Infrastructure;
using MassTransit;
using MassTransit.NewIdProviders;
using Microsoft.Extensions.DependencyInjection;

namespace Institutional.Api.Configurations;

public static class ApplicationSetup
{
    public static IServiceCollection AddApplicationSetup(this IServiceCollection services)
    {
        services.AddScoped<IContext, ApplicationDbContext>();
        NewId.SetProcessIdProvider(new CurrentProcessIdProvider());
        
        return services;
    }
    
    
}