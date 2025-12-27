using Shared.Data.Repositories;

namespace Shared.Mailing.Data.Repositories;

public interface IEmailTemplateRepository : IBaseEntityRepository<EmailTemplate, uint>
{
    Task<EmailTemplate> GetByNameAsync(string name);
}
