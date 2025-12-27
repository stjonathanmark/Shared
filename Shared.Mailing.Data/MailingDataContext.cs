using Microsoft.EntityFrameworkCore;

namespace Shared.Mailing.Data;

public class MailingDataContext : DbContext
{
    public DbSet<EmailTemplate> EmailTemplates { get; set; }

    public DbSet<EmailTemplateReplacement> EmailTemplateReplacements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        MailingDataMapper.Map(modelBuilder);
    }
}
