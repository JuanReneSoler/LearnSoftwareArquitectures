using Application.Services;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Infrastructure.Extentions;

namespace Infrastructure.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService TaskService)
    {
        _taskService = TaskService;
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> Get(int Id, CancellationToken cancellationToken)
    {
        var entiry = await _taskService.Filter(x => x.Id == Id, null, null, cancellationToken);
        return Ok(entiry.FirstOrDefault());
    }

    [HttpGet()]
    public async Task<IActionResult> List(int? GroupId, int? PersonId, CancellationToken cancellationToken)
    {
        Expression<Func<TaskDto, bool>> expression = (x) => x.Id > 0;

        if (GroupId > 0) expression = expression.And(x => x.GroupId == GroupId);

        if (PersonId > 0) expression = expression.And(x => x.PersonId == PersonId);

        var result = await _taskService.Filter(expression, null, null, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(TaskDto Task, CancellationToken cancellationToken)
    {
        var result = await _taskService.Create(Task, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(TaskDto Task, CancellationToken cancellationToken)
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
