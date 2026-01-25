using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.Security.Data;

public static class SecurityMapper
{
    public static void Map<TUserProfile, TUserId>(EntityTypeBuilder<TUserProfile> entity, string tableName = "UserProfiles", string schemaName = "dbo")
        where TUserProfile : BaseUserProfile<TUserId>
        where TUserId : struct, IEquatable<TUserId>
    {
        entity.ToTable(tableName, schemaName);

        entity.HasKey(p => p.Id);
    }
}
