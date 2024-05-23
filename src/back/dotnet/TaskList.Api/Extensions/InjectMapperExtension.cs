using Application.UsesCases;
using Domain.Entities;
using EasyMapper;

namespace TaskList.Api.Extensions;

public static class InjectMapperExtension
{
    public static IServiceCollection InjectMapper(this IServiceCollection services)
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
        return services;
    }
}
