using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.Identity.Roles.Commands;
using TaskManager.Application.Features.Identity.Roles.Queries.GetAll;

namespace TaskManager.Api.Controllers;

public class RolesController : BaseApiController
{
    // GET: api/<Roles>
    [HttpGet]
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
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<Roles>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }

}
