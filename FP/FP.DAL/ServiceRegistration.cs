using FP.Core.Entities;
using FP.Core.Repositories;
using FP.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FP.DAL;

public static class ServiceRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStadiumRepository, StadiumRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        return services;
    }
}