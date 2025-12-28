namespace Shared.Mailing;

public abstract class BaseEmailOptions
{
    public EmailTemplateOptions Template { get; set; } = new();

    public IList<KeyValuePair<string, string>> Administrators { get; set; } = [];
}
