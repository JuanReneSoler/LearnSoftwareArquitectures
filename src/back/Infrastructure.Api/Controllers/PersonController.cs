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
    public async Task<IActionResult> Find(int Id, CancellationToken cancellationToken)
    {
        var entiry = await _personService.Filter(x => x.Id == Id, null, null, cancellationToken);
        return Ok(entiry.FirstOrDefault());
    }

    [HttpGet()]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var result = await _personService.Filter(x => x.Id > 0, null, null, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] PersonDto Person, CancellationToken cancellationToken)
    {
        var result = await _personService.Create(Person, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PersonDto Person, CancellationToken cancellationToken)
    {
        var result = await _personService.Update(Person, Person.Id, cancellationToken);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
    {
        var result = await _personService.Delete(Id, cancellationToken);
        return Ok(result);
    }
}
