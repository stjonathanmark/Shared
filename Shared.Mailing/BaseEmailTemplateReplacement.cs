namespace Shared.Mailing;

public abstract class BaseEmailTemplateReplacement : BaseEntity<uint>
{
    public uint EmailTemplateId { get; set; }

    public string Key { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;
}
