namespace Shared.Mailing;

public class EmailTemplateReplacement : BaseEmailTemplateReplacement
{
    public EmailTemplate Template { get; set; } = new();
}
