namespace Shared.Mailing;

public class EmailTemplateReplacement : BaseEntity<int>
{
    public int EmailTemplateId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;
}
