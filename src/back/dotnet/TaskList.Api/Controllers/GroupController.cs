using Application.UsesCases;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Application.Extensions;
using TaskList.Api.Dtos;

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

    [HttpGet("{Id}")]
    public async Task<IActionResult> Find(int Id, CancellationToken cancellationToken)
    {
        var item = await _groupService.Find(Id, cancellationToken);
        return Ok(item);
    }

    [HttpGet()]
    public async Task<IActionResult> List(GroupFilter Filter)
    {
        var result = await _groupService.Filter(Filter.Search, Filter.page, Filter.size, Filter.cancellationToken);
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
