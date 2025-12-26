using Microsoft.Extensions.Options;
using Shared.Mailing.Data;

namespace Shared.Mailing;

public abstract class BaseEmailService : IBaseEmailService
{
    private readonly EmailOptions emailOpts;
    private readonly IMailingUnitOfWork mailingUow;

    public BaseEmailService(IOptions<EmailOptions> emailOptions, IMailingUnitOfWork mailingUnitOfWork)
    {
        emailOpts = emailOptions.Value;
        mailingUow = mailingUnitOfWork;
    }

    public virtual IEmailTemplate GetEmailTemplate()
    {
        return new EmailTemplate(emailOpts.Template);
    }

    public virtual IEmailTemplate GetEmailTemplate(string subject)
    {
        var template = GetEmailTemplate();
        template.Subject = subject;

        return template;
    }

    public virtual async Task<IEmailTemplate> GetEmailTemplateAsync(string templateName, string subject = "")
    {
        return emailOpts.Template.Location switch
        {
            EmailTemplateLocation.FileSystem => await GetFileBasedEmailTemplateAsync(subject, templateName),
            EmailTemplateLocation.Database => await GetDatabaseEmailTemplate(templateName),
            _ => throw new NotSupportedException($"The email template in '{emailOpts.Template.Location}' is either not or implemented supported."),
        };
    }

    private async Task<IEmailTemplate> GetFileBasedEmailTemplateAsync(string subject, string templateName)
    {
        var template = GetEmailTemplate(subject);
        template.HtmlBody = await File.ReadAllTextAsync($"{emailOpts.Template.File.HtmlPath}{templateName}.html");
        template.TextBody = await File.ReadAllTextAsync($"{emailOpts.Template.File.TextPath}{templateName}.txt");
        return template;
    }

    private async Task<IEmailTemplate> GetDatabaseEmailTemplate(string templateName)
    {
        var template = await mailingUow.EmailTemplates.GetByNameAsync(templateName);
        template.PlaceHolderBeginning = emailOpts.Template.PlaceHolderBeginning;
        template.PlaceHolderEnding = emailOpts.Template.PlaceHolderEnding;
        return template;
    }
}
