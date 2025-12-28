using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Mailing.Data;

namespace Shared.Mailing;

public static class SharedMailingExtensions
{
    public static IServiceCollection AddSharedMailing(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSharedMailingData(configuration)
            .Configure<SmtpOptions>(configuration.GetSection(SmtpOptions.SectionName))
            .AddScoped<IEmailService, SmtpService>();
    }
}
