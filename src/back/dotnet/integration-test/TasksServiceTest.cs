using Domain.Entities;
using Application.Dtos;
using Application.UsesCases;
using Infrastructure.EF;
using EasyMapper;

namespace integration_test;

[TestClass]
public class TasksServiceTest
{
    private readonly IMapper _mapper;
    private readonly TaskUseCase _taskUC;
    private readonly GroupsUseCase _groupUC;
    private readonly PersonUseCase _personUC;
    private static PersonDto _person = new PersonDto();
    private static TaskDto _task = new TaskDto();
    private static GroupDto _group = new GroupDto();
    private static CancellationToken _token = new CancellationToken();

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
        _taskUC = new TaskUseCase(repositoryTask, _mapper);
        _groupUC = new GroupsUseCase(repositoryGroup, _mapper);
        _personUC = new PersonUseCase(repositoryPerson, _mapper);
    }

    [TestMethod]
    public async Task Create()
    {
        var person = await _personUC.Create(new PersonDto
        {
            Name = "Juan Soler"
        }, _token);

        if (person is null) Assert.Fail();

        _person = person;

        var group = await _groupUC.Create(new GroupDto
        {
            Name = "Test"
        }, _token);

        if (group is null) Assert.Fail();
        _group = group;

        var task = await _taskUC.Create(new TaskDto
        {
            Description = "Esta tarea es una prueba, XD",
            GroupId = group?.Id ?? 0,
            Group = _group,
            Id = 0,
            PersonId = person?.Id ?? 0,
            Person = _person,
            Title = "Tarea de prueba"
        }, _token);

        if (task is null) Assert.Fail();

        _task = task;
    }

    [TestMethod]
    public async Task Read()
    {
        var tasks = await _taskUC.Filter(x => x.Id == _task.Id, 0, 0, _token);

        if (tasks.Items?.Count() is 0) Assert.Fail();
    }

    [TestMethod]
    public async Task Update()
    {
        _task.Description = "Esto es una prueba Editada";
        _task.Title = "Esto es una prueba (Edited)";

        var task = await _taskUC.Update(_task, _task.Id, _token);

        if (task is null) Assert.Fail();

        _task = task;
    }

    [TestMethod]
    public async Task Delete()
    {
        var result = await _taskUC.Delete(_task.Id, _token);
        var result2 = await _personUC.Delete(_person.Id, _token);
        var result3 = await _groupUC.Delete(_group.Id, _token);

        if (result is 0) Assert.Fail();
        if (result2 is 0) Assert.Fail();
        if (result3 is 0) Assert.Fail();
    }
}
