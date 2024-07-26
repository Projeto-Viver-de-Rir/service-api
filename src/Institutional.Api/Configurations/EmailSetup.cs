﻿using FluentEmail.Core.Interfaces;
using FluentEmail.Smtp;
using Institutional.Api.Email;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;

namespace Institutional.Api.Configurations;

public static class EmailSetup
{
    public static IServiceCollection AddEmailSetup(this IServiceCollection services, ConfigurationManager configuration)
    {
        var emailSettings = configuration.GetSection("EmailSettings");
        var defaultFromEmail = emailSettings["DefaultFromEmail"];
        var host = emailSettings["Host"];
        var port = emailSettings.GetValue<int>("Port");
        var username = emailSettings["Username"];
        var password = emailSettings["Password"];
        
        services.AddFluentEmail(defaultFromEmail);
        
        SmtpClient smtp = new()
        {
            Credentials = new NetworkCredential(username, password),
            EnableSsl = true,
            Host = host, 
            Port = port
        };
        
        services.AddSingleton<ISender>(x => new SmtpSender(smtp));
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IEmailSender, EmailSender>();
        
        return services;
    }
}