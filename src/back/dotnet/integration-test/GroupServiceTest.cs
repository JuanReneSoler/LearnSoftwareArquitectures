using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using EasyMapper;
using Application.Dtos;

namespace integration_test;

[TestClass]
public class GroupServiceTest
{
    private readonly IMapper _mapper;
    private readonly GroupService _service;
    private static GroupDto _group = new GroupDto();
    private static CancellationToken _token = new CancellationToken();

    public GroupServiceTest()
    {
        var context = new SqlServerContext();
        context.Database.EnsureCreated();
        var mapperConfig = new MapperConfiguration();
        mapperConfig.SetMapperProfile(x =>
        {
            x.CreateMap<Group, GroupDto>();
            x.CreateMap<GroupDto, Group>();
        });
        _mapper = mapperConfig.CreateMapper();
        var repository = new GenericRepository<Group>(context);
        _service = new GroupService(repository, _mapper);
    }

    [TestMethod]
    public async Task Create()
    {
        var group = await _service.Create(new GroupDto
        {
            Name = "Test"
        }, _token);

        if (group is null) Assert.Fail();

        _group = group;
    }

    [TestMethod]
    public async Task Read()
    {
        var groups = await _service.Filter(x => x.Id == _group.Id, 0, 0, _token);

        if (groups.Count() is 0) Assert.Fail();
    }

    [TestMethod]
    public async Task Update()
    {
        _group.Name = "Test Edited";
        var group = await _service.Update(_group, _group.Id, _token);

        if (group is null) Assert.Fail();

        _group = group;
    }

    [TestMethod]
    public async Task Delete()
    {
 
 
 
 
        var result =await _service.Delete(_group.Id, _token);

        if (result is 0) Assert.Fail();
    }
}
