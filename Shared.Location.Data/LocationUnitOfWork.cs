using Microsoft.EntityFrameworkCore;
using Shared.Data;
using Shared.Location.Data.Repositories;

namespace Shared.Location.Data;

public class LocationUnitOfWork : BaseUnitOfWork, ILocationUnitOfWork
{
    public LocationUnitOfWork(DbContext context)
        : base(context)
    { }

    public IAddressRepository Addresses => GetRepository<AddressRepository, Address>();

    public ICountryRepository Countries => GetRepository<CountryRepository, Country>();

    public IRegionRepository Regions => GetRepository<RegionRepository, Region>();
}
