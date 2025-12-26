using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;

namespace Shared.Mailing;

public class EmailTemplate : BaseEntity<int>, IEmailTemplate
{
    private readonly IDictionary<string, string> replacements = new Dictionary<string, string>();
    private readonly IList<LinkedResource> resources = [];

    public EmailTemplate() { }

    public EmailTemplate(EmailTemplateOptions options)
    {
        AssignOptions(options);
    }

    public string Name { get; set; } = string.Empty;

    public string FromAddress { get; set; } = string.Empty;

    public string FromDisplayName { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public IDictionary<string, string> ToAddresses { get; set; } = new Dictionary<string, string>();

    public string HtmlBody { get; set; } = string.Empty;

    public string TextBody { get; set; } = string.Empty;

    public string PlaceHolderBeginning { get; set; } = "{{";

    public string PlaceHolderEnding { get; set; } = "}}";

    public List<EmailTemplateReplacement> Replacements { get; set; } = [];

    public void AssignOptions(EmailTemplateOptions options)
    {
        FromAddress = options.FromAddress;
        FromDisplayName = options.FromDisplayName;
        PlaceHolderBeginning = options.PlaceHolderBeginning;
        PlaceHolderEnding = options.PlaceHolderEnding;
    }

    public MailMessage GetMailMessage()
    {
        var mailMsg = GetMessage();

        foreach (var toAddr in ToAddresses)
            mailMsg.To.Add(!string.IsNullOrWhiteSpace(toAddr.Value)
             ? new MailAddress(toAddr.Key, toAddr.Value)
             : new MailAddress(toAddr.Key));

        return mailMsg;
    }

    public IList<MailMessage> GetMailMessages()
    {
        var mailMsgs = new List<MailMessage>();

        foreach (var toAddr in ToAddresses)
        {
            var mailMsg = GetMessage();
            mailMsg.To.Add(!string.IsNullOrWhiteSpace(toAddr.Value)
             ? new MailAddress(toAddr.Key, toAddr.Value)
             : new MailAddress(toAddr.Key));
            mailMsgs.Add(mailMsg);
        }

        return mailMsgs;
    }

    public void AddImage(string filePath, string imageMediaType, string replacementKey)
    {
        var resource = new LinkedResource(filePath, imageMediaType);

        AddResource(resource, replacementKey);
    }

    public void AddImage(byte[] imageBytes, string imageMediaType, string replacementKey)
    {
        var resource = new LinkedResource(new MemoryStream(imageBytes), imageMediaType);

        AddResource(resource, replacementKey);
    }

    public void RemoveImage(string replacementKey)
    {
        var resource = resources.FirstOrDefault(r => r.ContentId == replacements[replacementKey]);

        if (resource != null)
        {
            resources.Remove(resource);
            replacements.Remove(replacementKey);
        }
    }

    public void ClearImages()
    {
        if (resources.Any())
        {
            foreach (var resource in resources)
            {
                if (replacements.Any(r => r.Value == resource.ContentId))
                    replacements.Remove(replacements.First(r => r.Value == resource.ContentId));
            }

            resources.Clear();
        }
    }

    public void AddReplacementValue(string key, string value)
    {
        replacements.TryAdd(key, value);
    }

    public void RemoveReplacementValue(string key)
    {
        replacements.Remove(key);
    }

    public void AddReplacementValues(IDictionary<string, string> replacementValues)
    {
        foreach (var replacement in replacementValues)
            AddReplacementValue(replacement.Key, replacement.Value);
    }

    public void RemoveReplacementValues(IEnumerable<string> keys)
    {
        foreach (var key in keys)
            RemoveReplacementValue(key);
    }

    public void ClearReplacementValues()
    {
        replacements.Clear();
    }

    public void Reset()
    {
        FromAddress =
            FromDisplayName =
            Subject =
            HtmlBody =
            TextBody = string.Empty;

        replacements.Clear();
        ToAddresses.Clear();
        resources.Clear();
    }

    protected virtual void AddResource(LinkedResource resource, string replacementKey)
    {
        resource.ContentId = Guid.NewGuid().ToString();

        resources.Add(resource);

        AddReplacementValue(replacementKey, resource.ContentId);
    }

    protected MailMessage GetMessage()
    {
        var mailMsg = new MailMessage();

        if (string.IsNullOrWhiteSpace(Subject))
            throw new Exception("Subject is required for all email messages.");

        if (string.IsNullOrWhiteSpace(FromAddress))
            throw new Exception("From email address is required to create mail message from email template.");

        if (!ToAddresses.Any())
            throw new Exception("At least one \"To\" email address is required to create mail message from email template.");

        if (string.IsNullOrWhiteSpace(HtmlBody) && string.IsNullOrWhiteSpace(TextBody))
            throw new Exception("Html or text body is required to create mail message from email template.");

        mailMsg.Subject = Subject;

        mailMsg.From = !string.IsNullOrWhiteSpace(FromDisplayName)
            ? new MailAddress(FromAddress, FromDisplayName)
            : new MailAddress(FromAddress);

        var textBody = GetTextBody();
        var htmlBody = GetHtmlBody();

        if (textBody != null)
            mailMsg.AlternateViews.Add(textBody);

        if (htmlBody != null)
        {
            mailMsg.IsBodyHtml = true;
            mailMsg.AlternateViews.Add(htmlBody);
        }

        return mailMsg;
    }

    protected virtual AlternateView? GetHtmlBody()
    {
        if (string.IsNullOrWhiteSpace(HtmlBody)) return null;

        var altView = AlternateView.CreateAlternateViewFromString(FillPlaceHolders(HtmlBody), null, MediaTypeNames.Text.Html);

        if (resources.Any())
            foreach (var resource in resources)
                altView.LinkedResources.Add(resource);

        return altView;
    }

    protected virtual AlternateView? GetTextBody()
    {
        if (string.IsNullOrWhiteSpace(TextBody)) return null;

        return AlternateView.CreateAlternateViewFromString(FillPlaceHolders(TextBody), null, MediaTypeNames.Text.Plain);
    }

    protected virtual string FillPlaceHolders(string body)
    {
        if (replacements.Any())
        {
            foreach (var replacement in replacements)
            {
                var regex = new Regex($"{PlaceHolderBeginning}[:space:]?{replacement.Key}[:space:]?{PlaceHolderEnding}");
                body = regex.Replace(body, replacement.Value);
            }
        }

        return body;
    }
}
