using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class InjectUsesCasesExtention
{
    public static IServiceCollection AddUsesCases(this IServiceCollection services)
    {
        services.AddScoped<ITaskUseCase, TaskUseCase>();
        services.AddScoped<IGroupUseCase, GroupsUseCase>();
        services.AddScoped<IPersonUseCase, PersonUseCase>();
        return services;
    }
}
