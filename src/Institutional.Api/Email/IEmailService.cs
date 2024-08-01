using System.Threading.Tasks;

namespace Institutional.Api.Email;

public interface IEmailService
{
    /// <summary>
    /// Send an email.
    /// </summary>
    /// <param name="emailMessage">Message object to be sent</param>
    Task SendEmailAsync(EmailMessageModel emailMessage);
}