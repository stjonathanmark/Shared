namespace Shared.Mailing;

public abstract class BaseEmailTemplate : BaseEntity<uint>
{
    public string Name { get; set; } = string.Empty;

    public string FromAddress { get; set; } = string.Empty;

    public string FromDisplayName { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string HtmlBody { get; set; } = string.Empty;

    public string TextBody { get; set; } = string.Empty;

    public string PlaceHolderBeginning { get; set; } = "{{";

    public string PlaceHolderEnding { get; set; } = "}}";
}
