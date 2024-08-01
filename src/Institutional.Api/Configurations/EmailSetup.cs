using Institutional.Api.Email;
using Institutional.Infrastructure.Email;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Institutional.Api.Configurations;

public static class EmailSetup
{
    public static IServiceCollection AddEmailSetup(this IServiceCollection services, ConfigurationManager configuration)
    {
        var emailSettings = configuration.GetSection("EmailSettings");

        var settings = new EmailSettings() {
            Host = emailSettings["Host"],
            Port = emailSettings.GetValue<int>("Port"),
            SenderName = emailSettings["SenderName"],
            SenderEmail = emailSettings["SenderEmail"],
            Username = emailSettings["Username"],
            Password = emailSettings["Password"]
        };
        
        services.AddSingleton<IEmailService>(x => new EmailService(settings));
        services.AddTransient<IEmailSender, EmailSender>();
        
        return services;
    }
}