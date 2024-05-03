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
    public IActionResult Get(int Id)
    {
        var entiry = _taskService.Filter(x => x.Id == Id, null, null).FirstOrDefault();
        return Ok(entiry);
    }

    [HttpGet()]
    public IActionResult List()
    {
        var result = _taskService.Filter(x => x.Id > 0, null, null);
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Add(TaskDto Task)
    {
        var result = _taskService.Create(Task);
        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(TaskDto Task)
    {
        var result = _taskService.Update(Task, Task.Id);
        return Ok(result);
    }

    [HttpPut("{Id}/ChangeGroup/{GroupId}")]
    public IActionResult Update(int Id, int GroupId)
    {
        var result = _taskService.ReasignToGroup(Id, GroupId);
        return Ok(result);
    }

    [HttpDelete]
    public IActionResult Delete(int Id)
    {
        var result = _taskService.Delete(Id);
        return Ok(result);
    }
}
