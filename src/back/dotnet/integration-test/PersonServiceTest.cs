using Application.Dtos;
using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.DependencyInyection;
using Application.DependencyInjection;

namespace integration_test;

[TestClass]
public class PersonServiceTest
{
    private readonly PersonUseCase _uc;
    private static PersonDto _person = new PersonDto();
    private static CancellationToken _token = new CancellationToken();

    public PersonServiceTest()
    {
        var serviceProvider = new ServiceCollection()
            .AddDbContext("")
            .AddUnitOfWork()
            .AddUsesCases()
            .AddMapper()
            .AddServices()
            .BuildServiceProvider();
        _uc = serviceProvider.GetService<PersonUseCase>();
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
