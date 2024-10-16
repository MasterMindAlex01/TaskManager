using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.Identity.Users.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaskManager.Api.Controllers;

public class UsersController : BaseApiController
{
    // GET: api/<Users>
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    // GET api/<Users>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var result = await Mediator.Send(command);
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
}
