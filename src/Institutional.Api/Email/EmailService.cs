﻿using FluentEmail.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Institutional.Api.Email;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly IFluentEmailFactory _fluentEmailFactory;

    public EmailService(ILogger<EmailService> logger, IFluentEmailFactory fluentEmailFactory)
    {
        _logger = logger;
        _fluentEmailFactory = fluentEmailFactory;
    }

    public async Task Send(EmailMessageModel emailMessageModel)
    {
        _logger.LogInformation("Sending email");
        
        await _fluentEmailFactory.Create().To(emailMessageModel.ToAddress)
            .Subject(emailMessageModel.Subject)
            .Body(emailMessageModel.Body, true) // true means this is an HTML format message
            .SendAsync();
    }
}