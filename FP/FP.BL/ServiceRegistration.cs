using FluentValidation;
using FluentValidation.AspNetCore;
using FP.BL.Services.Implements;
using FP.BL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FP.BL;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IStadiumService, StadiumService>();
        services.AddScoped<IReservationService, ReservationService>();
        return services;
    }
}