using Application;
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

    [HttpGet]
    public IActionResult Get(int? Id)
    {
        if (Id is null)
        {
            var result = _taskService.GetAll(null, null);
            return Ok(result);
        }
        else
        {
            var entiry = _taskService.Filter(x => x.Id == Id, null, null).FirstOrDefault();
            return Ok(entiry);
        }
    }

    [HttpPost]
    public IActionResult Add(TaskDto Task)
    {
        var result = _taskService.Add(Task);
        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(TaskDto Task)
    {
        var result = _taskService.Update(Task, Task.Id);
        return Ok(result);
    }

    [HttpDelete]
    public IActionResult Delete(int Id)
    {
        var result = _taskService.Delete(Id);
        return Ok(result);
    }
}
