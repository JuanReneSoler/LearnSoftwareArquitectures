using Application.Dtos;
using Application.UseCases;
using Domain.Entities;
using Domain.UnitsOfWork;
using EasyMapper;
using Infrastructure.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace integration_test;

[TestClass]
public class PersonServiceTest
{
    private readonly IMapper _mapper;
    private readonly PersonUseCase _uc;
    private static PersonDto _person = new PersonDto();
    private static CancellationToken _token = new CancellationToken();

    public PersonServiceTest()
    {
        var serviceProvider = new ServiceCollection()
            .AddDbContext<SqlServerContext>()
            .AddSingleton<IGenericUnitOfWork, GenericUnitOfWork>()
            .BuildServiceProvider();
        var context = serviceProvider.GetService<SqlServerContext>();
        context?.Database.EnsureCreated();
        var mapperConfig = new MapperConfiguration();
        mapperConfig.SetMapperProfile(x =>
        {
            x.CreateMap<Person, PersonDto>();
            x.CreateMap<PersonDto, Person>();
        });
        _mapper = mapperConfig.CreateMapper();
        var uow = serviceProvider.GetService<IGenericUnitOfWork>();
        _uc = new PersonUseCase(uow, _mapper);
    }

    [TestMethod]
    public async Task Create()
    {
        var person = await _uc.Create(new PersonDto
        {
            Name = "Juan Soler"
        }, _token);

        if (person is null) Assert.Fail();

        _person = person;
    }

    [TestMethod]
    public async Task Read()
    {
        var person = await _uc.Find(_person.Id, _token);
        if (person is null) Assert.Fail();
    }

    [TestMethod]
    public async Task Update()
    {
        _person.Name = "Juan René Soler";

        var person = await _uc.Update(_person, _person.Id, _token);

        if (person is null) Assert.Fail();

        _person = person;
    }

    [TestMethod]
    public async Task Delete()
    {
        var result = await _uc.Delete(_person.Id, _token);

        if (result is 0) Assert.Fail();
    }
}
