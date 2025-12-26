using Microsoft.EntityFrameworkCore;
using Shared.Data.Repositories;

namespace Shared.Mailing.Data.Repositories;

public class EmailTemplateRepository : BaseEntityRepository<EmailTemplate, int>, IEmailTemplateRepository
{
    public EmailTemplateRepository(MailingDataContext context) 
        : base(context)
    { }

    public async Task<EmailTemplate> GetByNameAsync(string name)
    {
        var template = await set.Include(e => e.Replacements).FirstOrDefaultAsync(et => et.Name == name);
        return template == null ? throw new InvalidOperationException($"No EmailTemplate found with name '{name}'.") : template;
    }
}
