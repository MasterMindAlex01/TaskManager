using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.Identity.Users.Commands;
using TaskManager.Application.Features.Identity.Users.Queries;

namespace TaskManager.Api.Controllers;

[Authorize(Roles = "Supervisor")]
public class UsersController : BaseApiController
{
    // GET: api/<Users>

    [HttpGet("GetAll")]
    public async Task<ActionResult> Get()
    {
        var result = await Mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }

    // GET api/<Users>/5
    [HttpGet("GetUser/{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var result = await Mediator.Send(new GetUserByIdQuery(id));
        return Ok(result);
    }

    // POST api/<Users>
    [HttpPost("Create")]
    public async Task<ActionResult> Create([FromBody] CreateUserCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    // POST api/<Users>
    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] CreateUserCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    // PUT api/<Users>/5
    [HttpPut("Update")]
    public async Task<ActionResult> Update([FromBody] UpdateUserCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    // DELETE api/<Users>/5
    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteUserCommand(id));
        return Ok(result);
    }

    // POST api/<Users>/1/roles
    [HttpPost("{id}/roles")]
    public async Task<ActionResult> AssignRolesAsync(Guid id, [FromBody] AssignUserRolesCommand command)
    {
        command.UserId = id;
        var result = await Mediator.Send(command);
        return Ok(result);
    }

}
