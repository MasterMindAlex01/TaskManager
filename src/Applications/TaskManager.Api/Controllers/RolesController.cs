using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.Identity.Roles.Commands;
using TaskManager.Application.Features.Identity.Roles.Queries;

namespace TaskManager.Api.Controllers;

[Authorize(Roles = "Supervisor")]
public class RolesController : BaseApiController
{
    // GET: api/<Roles>
    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAll()
    {
        var result = await Mediator.Send(new GetAllRolesQuery());
        return Ok(result);
    }

    // GET api/<Roles>/5
    [HttpGet("GetRol/{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var result = await Mediator.Send(new GetRoleByIdQuery(id));
        return Ok(result);
    }

    // POST api/<Roles>
    [HttpPost("Create")]
    public async Task<ActionResult> Create([FromBody] CreateRoleCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    // PUT api/<Roles>/5
    [HttpPut("Update")]
    public async Task<ActionResult> Update([FromBody] UpdateRoleCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    // DELETE api/<Roles>/5
    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteRoleCommand(id));
        return Ok(result);
    }

}
