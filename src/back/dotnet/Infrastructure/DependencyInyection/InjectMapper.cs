using Application.Dtos;
using Domain.Entities;
using Domain.Services;
using EasyMapper;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInyection;

public static class InjectMapper
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddScoped(typeof(IMapper), (x =>
        {
            var mapperConfig = new MapperConfiguration();
            mapperConfig.SetMapperProfile(profile =>
            {
                profile.CreateMap<Group, GroupDto>();
                profile.CreateMap<GroupDto, Group>();
                profile.CreateMap<Person, PersonDto>();
                profile.CreateMap<PersonDto, Person>();
                profile.CreateMap<Tasks, TaskDto>();
                profile.CreateMap<TaskDto, Tasks>();
            });
            return mapperConfig.CreateMapper();
        }));
        services.AddScoped<IMapperService, MapperService>();
        return services;
    }
}
