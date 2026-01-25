using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Payements;

namespace Shared.Payments.Data;

public static class PaymentsMapper
{
    public static void Map<TPaymentMethod, TUserId>(EntityTypeBuilder<TPaymentMethod> entity, string tableName = "PaymentMethods", string schemaName = "dbo")
        where TPaymentMethod : BasePaymentMethod<TUserId>
        where TUserId : IEquatable<TUserId>
    {
        entity.ToTable(tableName, schemaName);

        entity.HasKey(p => p.Id);

        entity.Property(p => p.FingerPrint).HasMaxLength(50).IsRequired();
        entity.Property(p => p.CardBrand).HasMaxLength(50).IsRequired();
        entity.Property(p => p.CardId).HasMaxLength(50).IsRequired();

        entity.Property(p => p.LastFourDigits).IsFixedLength().HasMaxLength(4).IsRequired();

        entity.Ignore(p => p.Expired);
        entity.Ignore(p => p.AlmostExpired);
    }
}
