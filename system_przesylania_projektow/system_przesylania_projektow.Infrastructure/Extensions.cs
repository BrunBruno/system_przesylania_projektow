using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using system_przesylania_projektow.Application.Services;
using system_przesylania_projektow.Infrastructure.EF;
using system_przesylania_projektow.Infrastructure.EF.Contexts;
using system_przesylania_projektow.Infrastructure.Jwt;
using system_przesylania_projektow.Infrastructure.Services;

namespace system_przesylania_projektow.Infrastructure;

public static class Extensions {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
        //services.AddAuthorizationBuilder();
        //services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("AppDb"));
        //services.AddIdentityCore<IdentityUser>()
        //    .AddEntityFrameworkStores<AppDbContext>()
        //    .AddApiEndpoints();

        services.AddHttpContextAccessor();

        services.AddPostgres(configuration);

        services.AddJwt(configuration);

        services.AddCors(options => {
            options.AddPolicy("FrontEndClient", builder => {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            });
        });

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserContextService, UserContextService>();


        return services;
    }
}
