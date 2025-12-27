using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Security.Authorization;

namespace Shared.Security.Data;

public class SecurityDataContext<TUser, TRole, Tkey> : IdentityDbContext<TUser, TRole, Tkey>
    where Tkey : IEquatable<Tkey>
    where TUser : BaseUser<Tkey>
    where TRole : BaseRole<Tkey>
{
    public SecurityDataContext(DbContextOptions<SecurityDataContext<TUser, TRole, Tkey>> options)
        : base(options)
    { }
}
