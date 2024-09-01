using Domain.UnitsOfWork;
using Infrastructure.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInyection;

public static class InjectUnitOfWork
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IGenericUnitOfWork, GenericUnitOfWork>();
        return services;
    }
}
