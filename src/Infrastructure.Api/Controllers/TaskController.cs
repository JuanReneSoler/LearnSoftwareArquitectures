using Application;
using Domain;
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

    [HttpGet("All")]
    public IActionResult GeAll()
    {
        var result = _taskService.GetAll(null, null);
        return Ok(result);
    }

    [HttpGet]
    public IActionResult Get(int Id)
    {
        var entiry = _taskService.Filter(x => x.Id == Id, null, null).FirstOrDefault();
        return Ok(entiry);
    }

    [HttpPost]
    public IActionResult Add(Work Task)
    {
        _taskService.Add(Task);
        return Ok("Opeation Successfull");
    }

    [HttpPut]
    public IActionResult Update(Work Task)
    {
        _taskService.Update(Task, Task.Id);
        return Ok("Update Successfull");
    }

    [HttpDelete]
    public IActionResult Delete(int Id)
    {
        _taskService.Delete(Id);
        return Ok("delete Successfull");
    }
}
