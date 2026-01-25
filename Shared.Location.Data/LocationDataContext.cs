using Microsoft.EntityFrameworkCore;

namespace Shared.Location.Data;

public class LocationDataContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }

    public DbSet<Country> Countries { get; set; }

    public DbSet<Region> Regions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        LocationDataMapper.Map(modelBuilder);
    }
}
