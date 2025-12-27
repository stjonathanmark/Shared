namespace Shared.Mailing.Dto;

public class EmailTemplateDto : BaseEmailTemplate
{
    public List<EmailTemplateReplacementDto> Replacements { get; set; } = [];
}
