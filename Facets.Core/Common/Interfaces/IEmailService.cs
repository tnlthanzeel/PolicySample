using Facets.Core.Common.Dtos;

namespace Facets.Core.Common.Interfaces;

public interface IEmailService
{
    Task SendEmailByQueue(EmailModel email);

    Task<string> GetEmailTemplate(string emailTemplateName);
}
