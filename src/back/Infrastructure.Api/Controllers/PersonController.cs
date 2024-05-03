using Application.Services;
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

    [HttpGet("{Id}")]
    public IActionResult Find(int Id)
    {
        var entiry = _personService.Filter(x => x.Id == Id, null, null).FirstOrDefault();
        return Ok(entiry);
    }

    [HttpGet()]
    public IActionResult List()
    {
        var result = _personService.Filter(x => x.Id > 0, null, null);
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Add([FromBody] PersonDto Person)
    {
        var result = _personService.Create(Person);
        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] PersonDto Person)
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
