using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;
using Shared.Security.Authorization;

namespace Shared.Security.Data;

public static class SharedSecurityDataExtensions
{
    public static IServiceCollection AddSharedSecurityData<TUser, TRole, TKey>(this IServiceCollection services, IConfiguration configuration)
        where TKey : IEquatable<TKey>
        where TUser : BaseUser<TKey>
        where TRole : BaseRole<TKey>
    {
        return services.AddSharedData<SecurityDataContext<TUser, TRole, TKey>>(configuration, "Security")
            .AddScoped<ISecurityUnitOfWork<TUser, TRole, TKey>, SecurityUnitOfWork<TUser, TRole, TKey>>();
    }
}
