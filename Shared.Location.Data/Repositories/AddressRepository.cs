using Microsoft.EntityFrameworkCore;
using Shared.Data.Repositories;

namespace Shared.Location.Data.Repositories;

public class AddressRepository : BaseEntityRepository<Address, ulong>, IAddressRepository
{
    public AddressRepository(DbContext context)
        : base(context)
    { }
}
