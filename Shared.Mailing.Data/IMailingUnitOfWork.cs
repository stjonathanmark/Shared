using Shared.Data;
using Shared.Mailing.Data.Repositories;

namespace Shared.Mailing.Data;

public interface IMailingUnitOfWork : IBaseUnitOfWork
{
    IEmailTemplateRepository EmailTemplates { get; }
}
