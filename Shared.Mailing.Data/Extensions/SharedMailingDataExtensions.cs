using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;

namespace Shared.Mailing.Data;

public static class SharedMailingDataExtensions
{
    public static IServiceCollection AddSharedMailingData(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSharedData<MailingDataContext>(configuration, "Mailing")
            .AddScoped<IMailingUnitOfWork, MailingUnitOfWork>();
    }
}
