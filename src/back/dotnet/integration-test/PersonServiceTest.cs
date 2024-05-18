using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using EasyMapper;
using Application.Dtos;

namespace integration_test;

[TestClass]
public class PersonServiceTest
{
    private readonly IMapper _mapper;
    private readonly PersonService _service;
    private static PersonDto _person = new PersonDto();
    private static CancellationToken _token = new CancellationToken();

    public PersonServiceTest()
    {
        var context = new SqlServerContext();
        context.Database.EnsureCreated();
        var mapperConfig = new MapperConfiguration();
        mapperConfig.SetMapperProfile(x =>
        {
            x.CreateMap<Person, PersonDto>();
            x.CreateMap<PersonDto, Person>();
        });
        _mapper = mapperConfig.CreateMapper();
        var repository2 = new GenericRepository<Person>(context);
        _service = new PersonService(repository2, _mapper);
    }

    [TestMethod]
    public async Task Create()
    {
        var person = await _service.Create(new PersonDto
        {
            Name = "Juan Soler"
        }, _token);

        if (person is null) Assert.Fail();

        _person = person;
    }

    [TestMethod]
    public async Task Read()
    {
        var persons = await _service.Filter(x => x.Id == _person.Id, 0, 0, _token);

        if (persons.Count() is 0) Assert.Fail();
    }

    [TestMethod]
    public async Task Update()
    {
        _person.Name = "Juan René Soler";

        var person = await _service.Update(_person, _person.Id, _token);

        if (person is null) Assert.Fail();

        _person = person;

    }

    [TestMethod]
    public async Task Delete()
    {
        var result = await _service.Delete(_person.Id, _token);

        if (result is 0) Assert.Fail();
    }
}
