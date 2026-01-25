using Shared.Data;
using Shared.Location.Data.Repositories;

namespace Shared.Location.Data;

public interface ILocationUnitOfWork : IBaseUnitOfWork
{
    IAddressRepository Addresses { get; }

    ICountryRepository Countries { get; }

    IRegionRepository Regions { get; }
}
