using Application.UseCases;

namespace TaskList.Api.Extensions;

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
