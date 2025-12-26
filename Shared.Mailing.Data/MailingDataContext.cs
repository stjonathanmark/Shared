using Microsoft.EntityFrameworkCore;

namespace Shared.Mailing.Data;

public class MailingDataContext : DbContext
{
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
}
