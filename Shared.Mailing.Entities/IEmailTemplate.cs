using System.Net.Mail;

namespace Shared.Mailing;

public interface IEmailTemplate
{
    string FromAddress { get; set; }

    string FromDisplayName { get; set; }

    string Subject { get; set; }

    IDictionary<string, string> ToAddresses { get; set; }

    string HtmlBody { set; }

    string TextBody { set; }

    void AssignOptions(EmailTemplateOptions options);

    void AddImage(string filePath, string imageMediaType, string replacementKey);

    void AddImage(byte[] imageBytes, string imageMediaType, string replacementKey);

    void RemoveImage(string contentId);

    void AddReplacementValue(string key, string value);

    void AddReplacementValues(IDictionary<string, string> replacementValues);

    void RemoveReplacementValue(string key);

    void RemoveReplacementValues(IEnumerable<string> keys);

    void ClearReplacementValues();

    MailMessage GetMailMessage();

    IList<MailMessage> GetMailMessages();

    void Reset();
}
