using Application;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService PersonService)
    {
        _personService = PersonService;
    }

    [HttpGet]
    public IActionResult Find(int? Id)
    {
        if (Id is null)
        {
            var result = _personService.GetAll(null, null);
            return Ok(result);
        }
        else
        {
            var entiry = _personService.Filter(x => x.Id == Id, null, null).FirstOrDefault();
            return Ok(entiry);
        }
    }

    [HttpPost]
    public IActionResult Add(PersonDto Person)
    {
        var result = _personService.Add(Person);
        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(PersonDto Person)
    {
        var result = _personService.Update(Person, Person.Id);
        return Ok(result);
    }

    [HttpDelete]
    public IActionResult Delete(int Id)
    {
        var result = _personService.Delete(Id);
        return Ok(result);
    }
}
