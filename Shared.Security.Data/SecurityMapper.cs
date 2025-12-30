using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.Security.Data;

public static class SecurityMapper
{
    public static void Map<TUserProfile, TId, TUserId>(EntityTypeBuilder<TUserProfile> entity, string tableName = "UserProfiles", string schemaName = "dbo")
        where TUserProfile : BaseUserProfile<TId, TUserId>
        where TId : struct
        where TUserId : IEquatable<TUserId>
    {
        entity.ToTable(tableName, schemaName);

        entity.HasKey(p => p.Id);
    }
}
