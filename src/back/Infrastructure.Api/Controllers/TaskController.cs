using Application.Services;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

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
        var entiry = await _taskService.Filter(x => x.Id == Id, null, null);
        return Ok(entiry.FirstOrDefault());
    }

    [HttpGet()]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        Console.WriteLine(cancellationToken.CanBeCanceled);
        Console.ReadKey();
        var result = await _taskService.Filter(x => x.Id > 0, null, null);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(TaskDto Task, CancellationToken cancellationToken)
    {
        var result = await _taskService.Create(Task);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(TaskDto Task, CancellationToken cancellationToken)
    {
        var result = await _taskService.Update(Task, Task.Id);
        return Ok(result);
    }

    [HttpPut("{Id}/ChangeGroup/{GroupId}")]
    public async Task<IActionResult> Update(int Id, int GroupId, CancellationToken cancellationToken)
    {
        var result = await _taskService.ReasignToGroup(Id, GroupId);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
    {
        var result = await _taskService.Delete(Id);
        return Ok(result);
    }
}
