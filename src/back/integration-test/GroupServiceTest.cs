using Application.Services;
using Domain.Entities;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories;
using EasyMapper;
using Application.Dtos;

namespace integration_test;

[TestClass]
public class GroupServiceTest
{
    private readonly IMapper _mapper;
    private readonly GroupService _service;
    private static GroupDto _group = new GroupDto();

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
    public void Create()
    {
        var group = _service.Create(new GroupDto
        {
            Name = "Test"
        });

        if (group is null) Assert.Fail();

        _group = group;
    }

    [TestMethod]
    public void Read()
    {
        var groups = _service.Filter(x => x.Id == _group.Id, 0, 0);

        if (groups.Count() is 0) Assert.Fail();
    }

    [TestMethod]
    public void Update()
    {
        _group.Name = "Test Edited";
        var group = _service.Update(_group, _group.Id);

        if (group is null) Assert.Fail();

        _group = group;
    }

    [TestMethod]
    public void Delete()
    {
        var result = _service.Delete(_group.Id);

        if (result is 0) Assert.Fail();
    }
}
