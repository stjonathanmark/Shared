using Microsoft.EntityFrameworkCore;
using Shared.Data.Repositories;

namespace Shared.Mailing.Data.Repositories;

public class EmailTemplateReplacementRepository : BaseEntityRepository<EmailTemplateReplacement, uint>, IEmailTemplateReplacementRepository
{
    public EmailTemplateReplacementRepository(DbContext context)
        : base(context)
    { }
}
