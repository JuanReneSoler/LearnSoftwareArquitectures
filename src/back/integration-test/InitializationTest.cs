using Application.Services;
using Domain.Entities;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories;
using EasyMapper;
using Application.Dtos;

namespace integration_test;

[TestClass]
public class InitializationTest
{
    private readonly SqlServerContext _context;
    private readonly IMapper _mapper;
    //
    public InitializationTest()
    {
        _context = new SqlServerContext();
        _context.Database.EnsureCreated();
        var mapperConfig = new MapperConfiguration();
        mapperConfig.SetMapperProfile(x =>
        {
            x.CreateMap<Person, PersonDto>();
            x.CreateMap<PersonDto, Person>();
            
            x.CreateMap<Group, GroupDto>();
            x.CreateMap<GroupDto, Group>();
            
            x.CreateMap<Tasks, TaskDto>();
            x.CreateMap<TaskDto, Tasks>();
        });
        _mapper = mapperConfig.CreateMapper();
    }

    [TestMethod]
    public void BuildAllRepositoriesTest()
    {
        var repository = new GenericRepository<Group>(_context);
        var groupService = new GroupService(repository, _mapper);
        //
        var repository2 = new GenericRepository<Person>(_context);
        var personService = new PersonService(repository2, _mapper);
        //
        var repository3 = new GenericRepository<Tasks>(_context);
        var taskService = new TaskService(repository3, _mapper);
    }

    [TestMethod]
    public void CreateDataTest()
    {
        //
    }
}
