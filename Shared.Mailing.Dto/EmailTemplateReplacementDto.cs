namespace Shared.Mailing.Dto;

public class EmailTemplateReplacementDto : BaseEmailTemplateReplacement
{
    public EmailTemplateDto Template { get; set; } = new();
}
