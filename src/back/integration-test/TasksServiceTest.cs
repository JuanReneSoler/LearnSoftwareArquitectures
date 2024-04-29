using Application.Services;
using Domain.Entities;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories;
using EasyMapper;
using Application.Dtos;

namespace integration_test;

[TestClass]
public class TasksServiceTest
{
    private readonly IMapper _mapper;
    private readonly TaskService _taskService;
    private readonly GroupService _groupService;
    private readonly PersonService _personService;
    private static PersonDto _person = new PersonDto();
    private static TaskDto _task = new TaskDto();
    private static GroupDto _group = new GroupDto();

    public TasksServiceTest()
    {
        var context = new SqlServerContext();
        context.Database.EnsureCreated();
        var mapperConfig = new MapperConfiguration();
        mapperConfig.SetMapperProfile(x =>
        {
            x.CreateMap<Tasks, TaskDto>();
            x.CreateMap<TaskDto, Tasks>();
            //
            x.CreateMap<PersonDto, Person>();
            x.CreateMap<Person, PersonDto>();
            //
            x.CreateMap<Group, GroupDto>();
            x.CreateMap<GroupDto, Group>();
        });
        _mapper = mapperConfig.CreateMapper();
        var repositoryTask = new GenericRepository<Tasks>(context);
        var repositoryGroup = new GenericRepository<Group>(context);
        var repositoryPerson = new GenericRepository<Person>(context);
        _taskService = new TaskService(repositoryTask, _mapper);
        _groupService = new GroupService(repositoryGroup, _mapper);
        _personService = new PersonService(repositoryPerson, _mapper);
    }

    [TestMethod]
    public void Create()
    {
        var person = _personService.Create(new PersonDto
        {
            Name = "Juan Soler"
        });

        if (person is null) Assert.Fail();

        _person = person;

        var group = _groupService.Create(new GroupDto
        {
            Name = "Test"
        });

        if (group is null) Assert.Fail();
        _group = group;

        var task = _taskService.Create(new TaskDto
        {
            Description = "Esta tarea es una prueba, XD",
            GroupId = group?.Id ?? 0,
            Group = _group,
            Id = 0,
            PersonId = person?.Id ?? 0,
            Person = _person,
            Title = "Tarea de prueba"
        });

        if (task is null) Assert.Fail();

        _task = task;
    }

    [TestMethod]
    public void Read()
    {
        var tasks = _taskService.Filter(x => x.Id == _task.Id, 0, 0);

        if (tasks.Count() is 0) Assert.Fail();
    }

    [TestMethod]
    public void Update()
    {
        _task.Description = "Esto es una prueba Editada";
        _task.Title = "Esto es una prueba (Edited)";

        var task = _taskService.Update(_task, _task.Id);

        if (task is null) Assert.Fail();

        _task = task;
    }

    [TestMethod]
    public void Delete()
    {
        var result = _taskService.Delete(_task.Id);
        var result2 = _personService.Delete(_person.Id);
        var result3 = _groupService.Delete(_group.Id);

        if (result is 0) Assert.Fail();
        if (result2 is 0) Assert.Fail();
        if (result3 is 0) Assert.Fail();
    }
}
