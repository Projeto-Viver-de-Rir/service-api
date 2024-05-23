using Institutional.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Institutional.Api.Configurations;

public static class MediatRSetup
{
    public static IServiceCollection AddMediatRSetup(this IServiceCollection services)
    {
        services.AddMediatR((config) =>
        {
            config.RegisterServicesFromAssemblyContaining(typeof(Institutional.Application.IAssemblyMarker));
            config.AddOpenBehavior(typeof(ValidationResultPipelineBehavior<,>));
        });
        


        return services;
    }
}