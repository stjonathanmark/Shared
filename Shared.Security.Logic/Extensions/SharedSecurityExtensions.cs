using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Mailing;
using Shared.Security.Authentication;
using Shared.Security.Authorization;
using Shared.Security.Data;

namespace Shared.Security;

public static class SharedSecurityExtensions
{
    public static IServiceCollection AddSharedSecurity<TDbContext, TUser, TRole, TKey>(this IServiceCollection services, IConfiguration configuration)
        where TDbContext : IdentityDbContext<TUser, TRole, TKey>
        where TKey : IEquatable<TKey>
        where TUser : BaseUser<TKey>
        where TRole : BaseRole<TKey>
    {
        services.AddIdentityCore<TUser>(opts =>
        {
            opts.User.RequireUniqueEmail = true;
            opts.SignIn.RequireConfirmedEmail = Convert.ToBoolean(configuration["Shared:Security:Authentication:RequireEmailConfirmation"]);

            opts.Password.RequireDigit = Convert.ToBoolean(configuration["Shared:Security:Authentication:Password:RequireDigit"]);
            opts.Password.RequireNonAlphanumeric = Convert.ToBoolean(configuration["Security:Authentication:Password:RequireNonAlphanumeric"]);
            opts.Password.RequiredLength = Convert.ToInt32(configuration["Shared:Security:Authentication:Password:RequiredLength"]);
            opts.Password.RequireUppercase = Convert.ToBoolean(configuration["Shared:Security:Authentication:Password:RequireUppercase"]);
            opts.Password.RequireLowercase = Convert.ToBoolean(configuration["Shared:Security:Authentication:Password:RequireLowercase"]);

        })
            .AddRoles<TRole>()
            .AddEntityFrameworkStores<TDbContext>();

        return services.AddSharedMailing(configuration)
            .AddSharedSecurityData<TDbContext, TUser, TRole, TKey>(configuration)
            .Configure<AuthenticationOptions>(configuration.GetSection(AuthenticationOptions.SectionName))
            .AddScoped<IAuthenticationService<TUser, TKey>, AuthenticationService<TUser, TKey>>()
            .Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName))
            .AddScoped<IJwtService<TUser, TKey>, JwtService<TUser, TKey>>();
    }
}
