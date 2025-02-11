using FP.Core.Repositories;
using FP.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FP.DAL;

public static class ServiceRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}