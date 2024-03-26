using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using system_przesylania_projektow.Infrastructure.EF.Contexts;
using system_przesylania_projektow.Infrastructure.EF.Options;
using system_przesylania_projektow.Shared.Options;
using Microsoft.EntityFrameworkCore;
using system_przesylania_projektow.Application.Repositories;
using system_przesylania_projektow.Infrastructure.EF.Repositories;

namespace system_przesylania_projektow.Infrastructure.EF;

public static class Extensions {
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration) {

        var options = configuration.GetOptions<PostgresOptions>("Postgres");

        services.AddDbContext<AppDbContext>(ctx => ctx.UseNpgsql(options.ConnectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectRpository, ProjectRpository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ISolutionRepository, SolutionRepository>();

        return services;
    }
}
