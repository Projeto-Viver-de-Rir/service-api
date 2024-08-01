using Institutional.Infrastructure.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;

namespace Institutional.Api.Email;

public class EmailService : IEmailService
{
    private readonly EmailSettings _config;
    
    public EmailService(EmailSettings config)
    {
        _config = config;
    }
    
    public async Task SendEmailAsync(EmailMessageModel emailMessage)
    {
        var email = new MimeMessage(); 
        email.From.Add(new MailboxAddress(_config.SenderName, _config.SenderEmail));
        email.To.Add(MailboxAddress.Parse(emailMessage.ToAddress));
        email.Subject = emailMessage.Subject;
        email.Body = new TextPart(TextFormat.Html) {Text = emailMessage.Body};

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_config.Host, _config.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_config.Username, _config.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}