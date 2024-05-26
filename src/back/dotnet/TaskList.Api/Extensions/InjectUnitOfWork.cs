using Domain.UnitsOfWork;
using Infrastructure.EntityFramework;

namespace TaskList.Api.Extensions;

public static class InjectUnitOfWorkExtention
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IGenericUnitOfWork, GenericUnitOfWork>();
        return services;
    }
}
