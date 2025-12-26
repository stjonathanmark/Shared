using Shared.Data;
using Shared.Mailing.Data.Repositories;

namespace Shared.Mailing.Data;

public class MailingUnitOfWork : BaseUnitOfWork, IMailingUnitOfWork
{
    public MailingUnitOfWork(MailingDataContext context) 
        : base(context)
    { }

    public IEmailTemplateRepository EmailTemplates => GetRepository<IEmailTemplateRepository, EmailTemplate>();
}
