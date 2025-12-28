using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Data;

public static class SharedDataExtensions
{
    public static IServiceCollection AddSharedData<TDbContext>(this IServiceCollection services, IConfiguration configuration, string libraryName)
        where TDbContext : DbContext
    {
        var dbProvider = configuration[$"Shared:{libraryName}:Data:DatabaseProvider"];
        var connStringName = configuration[$"Shared:{libraryName}:Data:ConnectionStringName"] ?? "DefaultConnection";
        var connString = configuration.GetConnectionString(connStringName);

        services.AddDbContext<TDbContext>(opts =>
        {
            switch (dbProvider)
            {
                case "SqlServer":
                    opts.UseSqlServer(connString);
                    break;
                case "Sqlite":
                    opts.UseSqlite(connString);
                    break;
                case "Oracle":
                    opts.UseOracle(connString);
                    break;
                case "MySql":
                    opts.UseMySQL(connString!);
                    break;
                case "Npgsql":
                    opts.UseNpgsql(connString);
                    break;
                default:
                    var inMemoryDbName = configuration[$"{libraryName}:Data:InMemory:DatabaseName"] ?? "MailingDatabase";
                    opts.UseInMemoryDatabase(inMemoryDbName);
                    break;
            }
        });

        return services;
    }
}
