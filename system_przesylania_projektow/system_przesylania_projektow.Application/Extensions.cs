using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using system_przesylania_projektow.Core.Entities;

namespace system_przesylania_projektow.Application;

public static class Extensions {
    public static IServiceCollection AddApplication(this IServiceCollection services) {

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}
