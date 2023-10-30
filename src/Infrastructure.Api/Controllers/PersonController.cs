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

    [HttpGet("All")]
    public IActionResult GetAll()
    {
        var result = _personService.GetAll(null, null);
        return Ok(result);
    }

    [HttpGet]
    public IActionResult Find(int Id)
    {
        var entiry = _personService.Filter(x => x.Id == Id, null, null).FirstOrDefault();
        return Ok(entiry);
    }

    [HttpPost]
    public IActionResult Add(PersonDto Person)
    {
        _personService.Add(Person);
        return Ok("Created Person Successfull");
    }

    [HttpPut]
    public IActionResult Update(PersonDto Person)
    {
        _personService.Update(Person, Person.Id);
        return Ok("Update Person Successfull");
    }

    [HttpDelete]
    public IActionResult Delete(int Id)
    {
        _personService.Delete(Id);
        return Ok("delete Successfull");
    }
}
