using Confectionery.API.Application.Commands.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Confectionery.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [Route("logIn")]
        public async Task<ActionResult<bool>> LogInAsync([FromBody] LogInCommand logInCommand)
        {
            // var result = await _mediator.Send(logInCommand);
            return Ok(true);
        }
    }
}
