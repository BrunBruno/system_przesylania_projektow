using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using system_przesylania_projektow.Infrastructure.EF.Contexts;
using system_przesylania_projektow.Infrastructure.EF.Options;
using system_przesylania_projektow.Shared.Options;
using Microsoft.EntityFrameworkCore;

namespace system_przesylania_projektow.Infrastructure.EF;

public static class Extensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {

        var options = configuration.GetOptions<PostgresOptions>("Postgres");

        services.AddDbContext<AppDbContext>(ctx => ctx.UseNpgsql(options.ConnectionString));

        // services.AddScoped<IUserRepository, UserRepository>();


        // services.AddScoped<TransactionSeeder>();

        return services;
    }
}
