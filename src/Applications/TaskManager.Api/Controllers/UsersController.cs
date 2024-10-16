using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.Identity.Users.Commands;
using TaskManager.Application.Features.Identity.Users.Queries;
using TaskManager.Shared.Authorization;

namespace TaskManager.Api.Controllers;

public class UsersController : BaseApiController
{
    // GET: api/<Users>
    [Authorize(Roles = ConstantRoles.Administrator)]
    [HttpGet("GetAll")]
    public async Task<ActionResult> Get()
    {
        var result = await Mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }

    // GET api/<Users>/5
    [Authorize(Roles = ConstantRoles.Administrator)]
    [HttpGet("GetUser/{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var result = await Mediator.Send(new GetUserByIdQuery(id));
        return Ok(result);
    }

    // POST api/<Users>
    [Authorize(Roles = ConstantRoles.Administrator)]
    [HttpPost("Create")]
    public async Task<ActionResult> Create([FromBody] CreateUserCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    // POST api/<Users>
    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    // PUT api/<Users>/5
    [Authorize(Roles = ConstantRoles.Administrator)]
    [HttpPut("Update")]
    public async Task<ActionResult> Update([FromBody] UpdateUserCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    // DELETE api/<Users>/5
    [Authorize(Roles = ConstantRoles.Administrator)]
    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteUserCommand(id));
        return Ok(result);
    }

    // POST api/<Users>/1/roles
    [Authorize(Roles = ConstantRoles.Administrator)]
    [HttpPost("{id}/roles")]
    public async Task<ActionResult> AssignRolesAsync(Guid id, [FromBody] AssignUserRolesCommand command)
    {
        command.UserId = id;
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [Authorize(Roles = $"{ConstantRoles.Administrator},{ConstantRoles.Supervisor}")]
    [HttpPost("{id}/tasks/{taskId}")]
    public async Task<ActionResult> AssignTasksAsync(Guid id, Guid taskId)
    {
        var result = await Mediator.Send(new AssignUserTaskCommand(id, taskId));
        return Ok(result);
    }

}
