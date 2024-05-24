using Domain.UnitsOfWork;
using Infrastructure.EF;

namespace TaskList.Api.Extensions;

public static class InjectUnitOfWorkExtention
{
    public static IServiceCollection InjectUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IGenericUnitOfWork, GenericUnitOfWork>();
        return services;
    }
}
