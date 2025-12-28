using Shared.Data;
using Shared.Security.Authorization;

namespace Shared.Security.Data;

public class SecurityUnitOfWork<TUser, TRole, TKey> : BaseUnitOfWork, ISecurityUnitOfWork<TUser, TRole, TKey>
    where TKey : IEquatable<TKey>
    where TUser : BaseUser<TKey>
    where TRole : BaseRole<TKey>
{
    public SecurityUnitOfWork(SecurityDataContext<TUser, TRole, TKey> context)
        : base(context)
    { }
}
