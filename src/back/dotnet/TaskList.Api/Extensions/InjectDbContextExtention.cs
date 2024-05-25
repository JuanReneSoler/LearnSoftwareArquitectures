using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace TaskList.Api.Extensions;

public static class InjectDbContextExtention
{
    public static IServiceCollection InjectDbContext(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<SqlServerContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        return services;
    }
}
