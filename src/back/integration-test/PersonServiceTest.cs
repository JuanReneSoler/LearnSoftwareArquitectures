using Application.Services;
using Domain.Entities;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories;
using EasyMapper;
using Application.Dtos;

namespace integration_test;

[TestClass]
public class PersonServiceTest
{
    private readonly IMapper _mapper;
    private readonly PersonService _service;
    private static PersonDto _person = new PersonDto();

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
    public void Create()
    {
        var person = _service.Create(new PersonDto
        {
            Name = "Juan Soler"
        });

        if (person is null) Assert.Fail();

        _person = person;
    }

    [TestMethod]
    public void Read()
    {
        var persons = _service.Filter(x => x.Id == _person.Id, 0, 0);

        if (persons.Count() is 0) Assert.Fail();
    }

    [TestMethod]
    public void Update()
    {
        _person.Name = "Juan René Soler";

        var person = _service.Update(_person, _person.Id);

        if (person is null) Assert.Fail();

        _person = person;

    }

    [TestMethod]
    public void Delete()
    {
        var result = _service.Delete(_person.Id);

        if (result is 0) Assert.Fail();
    }
}
