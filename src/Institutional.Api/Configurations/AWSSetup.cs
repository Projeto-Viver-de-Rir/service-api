using Institutional.Application.Common;
using Institutional.Infrastructure.AWS;
using Institutional.Infrastructure.AWS.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Institutional.Api.Configurations;

public static class AWSSetup
{
    public static IServiceCollection AddAWSSetup(this IServiceCollection services, ConfigurationManager configuration)
    {
        var AWSConfiguration = configuration.GetSection("AWSConfiguration");
            
        var cred = new AWSCredentials() {
            AccessKey = AWSConfiguration["AccessKey"],
            SecretKey = AWSConfiguration["SecretKey"]
        };
        
        services.AddTransient<IStorageService>(x => new StorageService(cred));
        
        return services;
    }
}