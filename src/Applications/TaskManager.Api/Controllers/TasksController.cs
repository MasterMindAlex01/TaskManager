using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.TaskManager.Tasks.Commands;
using TaskManager.Application.Features.TaskManager.Tasks.Queries;

namespace TaskManager.Api.Controllers;

[Authorize]
public class TasksController : BaseApiController
{
    // GET: api/<TasksController>
    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAll([FromQuery] int pageNumber, int pageSize)
    {
        var result = await Mediator.Send(new GetAllTasksQuery(pageNumber, pageSize));
        return Ok(result);
    }

    // GET api/<TasksController>/5
    [HttpGet("GetTask/{id}")]
    public async Task<ActionResult> GetTask(Guid id)
    {
        var result = await Mediator.Send(new GetTaskByIdQuery(id));
        return Ok(result);
    }

    // POST api/<TasksController>
    [HttpPost("Create")]
    public async Task<ActionResult> Create([FromBody] CreateTaskCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    // PUT api/<TasksController>/5
    [HttpPut("Update")]
    public async Task<ActionResult> Update([FromBody] UpdateTaskCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    // DELETE api/<TasksController>/5
    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteTaskCommand(id));
        return Ok(result);
    }
}
