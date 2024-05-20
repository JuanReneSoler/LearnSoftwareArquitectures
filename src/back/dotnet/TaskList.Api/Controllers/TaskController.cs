using Application.UsesCases;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Application.Extensions;
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

    [HttpGet()]
    public async Task<IActionResult> List([FromQuery] TasksFilter Filter)
    {
        Expression<Func<TaskDto, bool>> expression = (x) => x.Id > 0;

        if (Filter.GroupId > 0) expression = expression.And(x => x.GroupId == Filter.GroupId);

        if (Filter.PersonId > 0) expression = expression.And(x => x.PersonId == Filter.PersonId);

        var result = await _taskService.Filter(expression, Filter.page, Filter.size, Filter.cancellationToken);
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
