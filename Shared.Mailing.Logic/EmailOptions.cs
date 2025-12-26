namespace Shared.Mailing;

public class EmailOptions
{
    public EmailTemplateOptions Template { get; set; } = new();

    public IList<KeyValuePair<string, string>> Administrators { get; set; } = [];
}
