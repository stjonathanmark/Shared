using Microsoft.EntityFrameworkCore;
using Shared.Data.Repositories;

namespace Shared.Location.Data.Repositories;

public class CountryRepository : BaseEntityRepository<Country, byte>, ICountryRepository
{
    public CountryRepository(DbContext context)
        : base(context)
    { }
}
