using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Minimal.Application.Interfaces.Services;
using Minimal.Infrastructure.Persistence;
using Moq;

namespace Minimal.Test.Helpers;

public class ApiWebApplicationFactory : WebApplicationFactory<Program> 
{
    public readonly Mock<IAdministratorService> AdministratorServiceMock = new();
    public readonly Mock<IVehicleService> VehicleServiceMock = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing"); 

        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }

            services.RemoveAll<IAdministratorService>();
            services.RemoveAll<IVehicleService>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryTestDb");
            });

            services.AddScoped<IAdministratorService>(_ => AdministratorServiceMock.Object);
            services.AddScoped<IVehicleService>(_ => VehicleServiceMock.Object);
        });
    }
}
