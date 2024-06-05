using Application.Common.Services;
using Serilog;

namespace Infrastructure.Emails;
internal class EmailService(ILogger logger) : IEmailService
{
    public Task SendEmailAsync(string email, string subject, string address)
    {
        // Logic for sending email
        logger.Information($"An email with the subject{subject}, and the content of {email}, was sent to {address}");
        return Task.CompletedTask;
    }
}

