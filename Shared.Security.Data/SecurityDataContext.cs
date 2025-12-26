using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Shared.Security.Data;

public class SecurityDataContext<TUser, TRole, TId> : IdentityDbContext<TUser, TRole, TId>
    where TId : struct, IEquatable<TId>
    where TUser : IdentityUser<TId>
    where TRole : IdentityRole<TId>
{
    public SecurityDataContext(DbContextOptions<SecurityDataContext<TUser, TRole, TId>> options)
        : base(options)
    { }
}
