using Microsoft.Extensions.Options;
using Shared.Mailing.Data;
using System.Net;
using System.Net.Mail;

namespace Shared.Mailing;

public class SmtpService : BaseEmailService, IEmailService
{
    private readonly SmtpOptions smtpOpts;
    private readonly SmtpClient client;

    public SmtpService(IOptions<SmtpOptions> smtpOptions, IMailingUnitOfWork mailingUnitOfWork)
        : base(smtpOptions, mailingUnitOfWork)
    {
        smtpOpts = smtpOptions.Value;
        client = new SmtpClient(smtpOpts.Host, smtpOpts.Port)
        {
            Credentials = new NetworkCredential(smtpOpts.Username, smtpOpts.Password),
            EnableSsl = smtpOpts.UseSsl
        };
    }

    public void Send(IEmailTemplate emailTemplate, bool sendSeparately = false)
    {
        if (sendSeparately)
        {
            var mailMsgs = emailTemplate.GetMailMessages();

            foreach (var mailMsg in mailMsgs)
                client.Send(mailMsg);
        }
        else
        {
            client.Send(emailTemplate.GetMailMessage());
        }
    }

    public void Send(string to, string from, string subject, string body, bool isHtml = true)
    {
        var mailMsg = new MailMessage(to, from, subject, body)
        {
            IsBodyHtml = isHtml
        };
        client.Send(mailMsg);
    }

    public void SendToAdministrators(IEmailTemplate emailTemplate, bool sendSeperately = false)
    {
        emailTemplate.ToAddresses = smtpOpts.Administrators.ToDictionary(admin => admin.Key, admin => admin.Value);
        Send(emailTemplate, sendSeperately);
    }
}
