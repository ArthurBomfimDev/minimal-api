using Minimal.Application.Interfaces.Repositories;
using Minimal.Application.Interfaces.Services;
using Minimal.Application.Interfaces.UnitOfWork;
using Minimal.Application.Services;
using Minimal.Infrastructure.Repositories;
using Minimal.Infrastructure.UnitOfWork;

namespace Minimal.Api.Extensions;

public static class InjectionDependencyExtension
{
    public static IServiceCollection ConfigureInjectionDependency(this IServiceCollection services)
    {
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IAdministratorRepository, AdministratorRepository>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IAdministratorService, AdministratorService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IPasswordEncryptorService, PasswordEncryptorService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}