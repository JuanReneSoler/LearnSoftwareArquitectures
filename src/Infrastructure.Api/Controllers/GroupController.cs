using Application;
using Domain;
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

    [HttpGet("All")]
    public IActionResult GetAll()
    {
        var result = _groupService.GetAll(null, null);
        return Ok(result);
    }

    [HttpGet]
    public IActionResult Find(int Id)
    {
        var entiry = _groupService.Filter(x => x.Id == Id, null, null).FirstOrDefault();
        return Ok(entiry);
    }

    [HttpPost]
    public IActionResult Add(Group Group)
    {
        _groupService.Add(Group);
        return Ok("Group Created Successfull");
    }

    [HttpPut]
    public IActionResult Update(Group Group)
    {
        _groupService.Update(Group, Group.Id);
        return Ok("Update Group Successfull");
    }

    [HttpDelete]
    public IActionResult Delete(int Id)
    {
        _groupService.Delete(Id);
        return Ok("delete group Successfull");
    }
}
