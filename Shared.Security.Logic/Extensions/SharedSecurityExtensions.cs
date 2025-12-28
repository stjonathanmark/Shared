using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Mailing;
using Shared.Security.Authentication;
using Shared.Security.Authorization;
using Shared.Security.Data;

namespace Shared.Security;

public static class SharedSecurityExtensions
{
    public static IServiceCollection AddSharedSecurity<TUser, TRole, TKey>(this IServiceCollection services, IConfiguration configuration)
        where TKey : IEquatable<TKey>
        where TUser : BaseUser<TKey>
        where TRole : BaseRole<TKey>
    {
        return services.AddSharedMailing(configuration)
            .AddSharedSecurityData<TUser, TRole, TKey>(configuration)
            .Configure<AuthenticationOptions>(configuration.GetSection(AuthenticationOptions.SectionName))
            .AddScoped<IAuthenticationService<TUser, TKey>, AuthenticationService<TUser, TKey>>()
            .Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName))
            .AddScoped<IJwtService<TUser, TKey>, JwtService<TUser, TKey>>();
    }
}
