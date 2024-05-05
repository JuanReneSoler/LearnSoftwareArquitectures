using Application.Services;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService GroupService)
    {
        _groupService = GroupService;
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> Find(int Id, CancellationToken cancellationToken)
    {
        var entiry = await _groupService.Filter(x => x.Id == Id, null, null);
        return Ok(entiry.FirstOrDefault());
    }

    [HttpGet()]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var result = await _groupService.Filter(x => x.Id > 0, null, null);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] GroupDto Group, CancellationToken cancellationToken)
    {
        var result = await _groupService.Create(Group);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] GroupDto Group)
    {
        var result = await _groupService.Update(Group, Group.Id);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
    {
        var result = await _groupService.Delete(Id);
        return Ok(result);
    }
}
