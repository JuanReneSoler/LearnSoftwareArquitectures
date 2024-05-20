using Application.UsesCases;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TaskList.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonUseCase _personService;

    public PersonController(IPersonUseCase PersonService)
    {
        _personService = PersonService;
    }

    [HttpGet()]
    public async Task<IActionResult> List([FromQuery, Required] int page, [FromQuery, Required] int size, CancellationToken cancellationToken)
    {
        var result = await _personService.Filter(x => x.Id > 0, page, size, cancellationToken);
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

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
    {
        var result = await _personService.Delete(Id, cancellationToken);
        return Ok(result);
    }
}
