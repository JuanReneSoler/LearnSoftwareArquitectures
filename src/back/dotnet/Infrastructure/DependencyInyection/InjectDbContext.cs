using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInyection;

public static class InjectDbContext
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<SqlServerContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        return services;
    }
}
