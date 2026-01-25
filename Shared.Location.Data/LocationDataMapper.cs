using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.Location.Data;

public static class LocationDataMapper
{
    public static void Map(ModelBuilder modelBuilder)
    {
        Map(modelBuilder.Entity<Address>());
        Map(modelBuilder.Entity<Country>());
        Map(modelBuilder.Entity<Region>());
    }

    public static void Map(EntityTypeBuilder<Address> entity, string tableName = "Addresses", string schemaName = "dbo")
    {
        entity.ToTable(tableName, schemaName);

        entity.Property(a => a.StreetOne).HasMaxLength(150);
        entity.Property(a => a.StreetTwo).HasMaxLength(150);
        entity.Property(a => a.City).HasMaxLength(150);
        entity.Property(a => a.ZipCode).HasMaxLength(25);
    }

    public static void Map(EntityTypeBuilder<Country> entity, string tableName = "Countries", string schemaName = "dbo")
    {
        entity.ToTable(tableName, schemaName);

        entity.Property(c => c.Name).HasMaxLength(150).IsRequired();
        entity.Property(c => c.Code).HasMaxLength(10).IsRequired();

        entity.HasMany(c => c.Regions).WithOne(r => r.Country).HasForeignKey(r => r.CountryId).OnDelete(DeleteBehavior.Restrict);
        entity.HasMany(c => c.Addresses).WithOne(a => a.Country).HasForeignKey(a => a.CountryId).OnDelete(DeleteBehavior.Restrict);
    }

    public static void Map(EntityTypeBuilder<Region> entity, string tableName = "Regions", string schemaName = "dbo")
    {
        entity.ToTable(tableName, schemaName);

        entity.Property(r => r.Name).HasMaxLength(150).IsRequired();
        entity.Property(r => r.Code).HasMaxLength(10).IsRequired();

        entity.HasMany(r => r.Addresses).WithOne(a => a.Region).HasForeignKey(a => a.RegionId).OnDelete(DeleteBehavior.Restrict);
    }
}
