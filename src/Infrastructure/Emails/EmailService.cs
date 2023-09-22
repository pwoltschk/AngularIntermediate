using Application.Common.Services;

namespace Infrastructure.Emails;
internal class EmailService : IEmailService
{
    public Task SendEmailAsync(string email, string subject, string adress)
    {
        // Logic for sending email
        return Task.CompletedTask;
    }
}

