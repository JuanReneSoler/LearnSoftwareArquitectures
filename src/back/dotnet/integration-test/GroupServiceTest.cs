using Domain.Entities;
using Application.UsesCases;
using Infrastructure.EF;
using EasyMapper;

namespace integration_test;

[TestClass]
public class GroupServiceTest
{
    private readonly IMapper _mapper;
    private readonly GroupsUseCase _uc;
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
        _uc = new GroupsUseCase(repository, _mapper);
    }

    [TestMethod]
    public async Task Create()
    {
        var group = await _uc.Create(new GroupDto
        {
            Name = "Test"
        }, _token);

        if (group is null) Assert.Fail();

        _group = group;
    }

    [TestMethod]
    public async Task Read()
    {
        var group = await _uc.Find(_group.Id, _token);
        if (group is null) Assert.Fail();
    }

    [TestMethod]
    public async Task Update()
    {
        _group.Name = "Test Edited";
        var group = await _uc.Update(_group, _group.Id, _token);

        if (group is null) Assert.Fail();

        _group = group;
    }

    [TestMethod]
    public async Task Delete()
    {
        var result = await _uc.Delete(_group.Id, _token);

        if (result is 0) Assert.Fail();
    }
}
