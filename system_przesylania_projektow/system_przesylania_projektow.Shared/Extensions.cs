using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using system_przesylania_projektow.Shared.Middleware;

namespace system_przesylania_projektow.Shared;

public static class Extensions {
    public static IServiceCollection AddShared(this IServiceCollection services) {
        services.AddScoped<ExceptionMiddleware>();
        return services;
    }

    public static IApplicationBuilder UseShared(this IApplicationBuilder app) {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}
