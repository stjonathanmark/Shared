using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;
using Shared.Security.Authorization;

namespace Shared.Security.Data;

public static class SharedSecurityDataExtensions
{
    public static IServiceCollection AddSharedSecurityData<TDbContext, TUser, TRole, TKey>(this IServiceCollection services, IConfiguration configuration)
        where TDbContext : IdentityDbContext<TUser, TRole, TKey>
        where TKey : IEquatable<TKey>
        where TUser : BaseUser<TKey>
        where TRole : BaseRole<TKey>
    {
        return services.AddSharedData<TDbContext>(configuration, "Security")
            .AddScoped<TDbContext>();
    }
}
