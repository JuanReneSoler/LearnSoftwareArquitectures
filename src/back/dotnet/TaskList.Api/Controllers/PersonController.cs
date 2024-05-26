using Application.Dtos;
using Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskList.Api.Dtos;

namespace TaskList.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PersonController : ControllerBase
{
    private readonly IPersonUseCase _personService;

    public PersonController(IPersonUseCase PersonService)
    {
        _personService = PersonService;
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> Find(int Id, CancellationToken cancellationToken)
    {
        var entity = await _personService.Find(Id, cancellationToken);
        return Ok(entity);
    }

    [HttpGet()]
    public async Task<IActionResult> List([FromQuery] PersonFilter Filter)
    {
        var result = await _personService.Filter(Filter.Search, Filter.page, Filter.size, Filter.cancellationToken);
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
