namespace Shared.Mailing;

public interface IBaseEmailService
{
    IEmailTemplate GetEmailTemplate();

    IEmailTemplate GetEmailTemplate(string subject);

    Task<IEmailTemplate> GetEmailTemplateAsync(string templateName, string subject = "");
}
