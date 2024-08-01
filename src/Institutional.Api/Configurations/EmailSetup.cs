using FluentEmail.Core.Interfaces;
using FluentEmail.MailKitSmtp;
using FluentEmail.Smtp;
using Institutional.Api.Email;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;
using System.Net.Mail;

namespace Institutional.Api.Configurations;

public static class EmailSetup
{
    public static IServiceCollection AddEmailSetup(this IServiceCollection services, ConfigurationManager configuration)
    {
        var emailSettings = configuration.GetSection("EmailSettings");
        var defaultFromEmail = emailSettings["DefaultFromEmail"];

        var smtp = new SmtpClientOptions
        {
            Server = emailSettings["Host"],
            Port = emailSettings.GetValue<int>("Port"),
            User = emailSettings["Username"],
            Password = emailSettings["Password"],
            UseSsl = true,
            RequiresAuthentication = true,
            SocketOptions = SecureSocketOptions.Auto
        };

        services.AddFluentEmail(defaultFromEmail);
        services.TryAdd(ServiceDescriptor.Singleton<ISender>(_ => new MailKitSender(smtp)));
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IEmailSender, EmailSender>();
        
        return services;
    }
}