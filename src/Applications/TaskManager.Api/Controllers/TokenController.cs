using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.Identity.Tokens.Queries.GetToken;

namespace TaskManager.Api.Controllers;

public class TokenController : BaseApiController
{
    // POST api/Token
    [HttpPost("login")]
    public async Task<ActionResult> Post([FromBody] GetTokenQuery command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("Autenticated")]
    public ActionResult Autenticated()
    {

        var data = HttpContext.User.Claims.Select(x => new
        {
            x.Value,
            x.Type
        });

        return Ok(new
        {
            data
        });
    }
}
