using Application.UsesCases;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TaskList.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupController : ControllerBase
{
    private readonly IGroupUseCase _groupService;

    public GroupController(IGroupUseCase GroupService)
    {
        _groupService = GroupService;
    }

    [HttpGet()]
    public async Task<IActionResult> List([FromQuery, Required] int page, [FromQuery, Required] int size, CancellationToken cancellationToken)
    {
        var result = await _groupService.Filter(x => x.Id > 0, page, size, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] GroupDto Group, CancellationToken cancellationToken)
    {
        var result = await _groupService.Create(Group, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] GroupDto Group, CancellationToken cancellationToken)
    {
        var result = await _groupService.Update(Group, Group.Id, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
    {
        var result = await _groupService.Delete(Id, cancellationToken);
        return Ok(result);
    }
}
