using Application.Dtos;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using TaskList.Api.Dtos;

namespace TaskList.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskUseCase _taskService;

    public TaskController(ITaskUseCase TaskService)
    {
        _taskService = TaskService;
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> Find(int Id, CancellationToken cancellationToken)
    {
        var entity = await _taskService.Find(Id, cancellationToken);
        return Ok(entity);
    }

    [HttpGet()]
    public async Task<IActionResult> List([FromQuery] TasksFilter Filter)
    {
        var result = await _taskService.Filter(Filter.GroupId, Filter.PersonId, Filter.Search, Filter.page, Filter.size, Filter.cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] TaskDto Task, CancellationToken cancellationToken)
    {
        var result = await _taskService.Create(Task, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TaskDto Task, CancellationToken cancellationToken)
    {
        var result = await _taskService.Update(Task, Task.Id, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{Id}/ChangeGroup/{GroupId}")]
    public async Task<IActionResult> Update(int Id, int GroupId, CancellationToken cancellationToken)
    {
        var result = await _taskService.ReasignToGroup(Id, GroupId, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
    {
        var result = await _taskService.Delete(Id, cancellationToken);
        return Ok(result);
    }
}
