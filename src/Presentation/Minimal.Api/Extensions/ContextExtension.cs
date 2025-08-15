using Microsoft.EntityFrameworkCore;
using Minimal.Infrastructure.Persistence;

namespace Minimal.Api.Extensions;

public static class ContextExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}