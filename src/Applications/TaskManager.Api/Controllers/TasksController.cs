using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.TaskManager.Tasks.Commands;
using TaskManager.Application.Features.TaskManager.Tasks.Queries;
using TaskManager.Application.Features.TaskManager.Tasks.Queries.GetAll;
using TaskManager.Shared.Authorization;

namespace TaskManager.Api.Controllers;

[Authorize(Roles = ConstantRoles.Administrator)]
public class TasksController : BaseApiController
{
    // GET: api/<TasksController>
    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAll([FromQuery] int pageNumber, int pageSize)
    {
        var result = await Mediator.Send(new GetAllTasksQuery(pageNumber, pageSize));
        return Ok(result);
    }
    
    [Authorize(Roles = $"{ConstantRoles.Administrator},{ConstantRoles.Supervisor},{ConstantRoles.Employee}")]
    [HttpGet("GetAllByCurrentUser")]
    public async Task<ActionResult> GetAllByCurrentUser()
    {
        var result = await Mediator.Send(new GetAllTasksByCurrentUserQuery());
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

    // PUT api/<TasksController>/5
    [Authorize(Roles = $"{ConstantRoles.Administrator},{ConstantRoles.Supervisor},{ConstantRoles.Employee}")]
    [HttpPut("UpdateStatus")]
    public async Task<ActionResult> UpdateStatus([FromBody] UpdateTaskStatusCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

}
