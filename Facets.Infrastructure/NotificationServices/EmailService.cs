using Facets.Core.Common.Dtos;
using Facets.Core.Common.Interfaces;

namespace Facets.Infrastructure.NotificationServices;

public sealed class EmailService : IEmailService
{
    public Task<string> GetEmailTemplate(string emailTemplateName)
    {
        throw new NotImplementedException();
    }

    public Task SendEmailByQueue(EmailModel email)
    {
        throw new NotImplementedException();
    }
}
