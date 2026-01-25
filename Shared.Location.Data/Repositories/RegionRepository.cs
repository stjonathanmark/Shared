using Microsoft.EntityFrameworkCore;
using Shared.Data.Repositories;

namespace Shared.Location.Data.Repositories;

public class RegionRepository : BaseEntityRepository<Region, ushort>, IRegionRepository
{
    public RegionRepository(DbContext context)
        : base(context)
    { }
}
