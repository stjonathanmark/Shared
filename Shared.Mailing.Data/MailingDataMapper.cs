using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.Mailing.Data;

public static class MailingDataMapper
{
    public static void Map(EntityTypeBuilder<EmailTemplate> entity, string tableName = "EmailTemplates", string schemaName = "dbo")
    {
        entity.ToTable(tableName, schemaName);
        entity.HasKey(e => e.Id);

        entity.HasIndex(e => e.Name).IsUnique();

        entity.Property(e => e.FromAddress).HasMaxLength(75).IsRequired();
        entity.Property(e => e.FromDisplayName).HasMaxLength(100).IsRequired();
        entity.Property(e => e.Subject).HasMaxLength(150).IsRequired();
        entity.Property(e => e.Name).HasMaxLength(75).IsRequired();

        entity.Property(e => e.PlaceHolderBeginning).HasMaxLength(5).IsRequired();
        entity.Property(e => e.PlaceHolderEnding).HasMaxLength(5).IsRequired();

        entity.Property(e => e.HtmlBody).IsRequired(false);
        entity.Property(e => e.TextBody).IsRequired(false);

    }
}
