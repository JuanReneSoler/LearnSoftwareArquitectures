using Domain.Entities;
using Domain.Repositories;
using Infrastructure.EF;

namespace TaskList.Api.Extensions;

public static class InjectRepositoriesExtention
{
    public static IServiceCollection InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGenericRepository<Tasks>, GenericRepository<Tasks>>();
        services.AddScoped<IGenericRepository<Person>, GenericRepository<Person>>();
        services.AddScoped<IGenericRepository<Group>, GenericRepository<Group>>();
        return services;
    }
}
