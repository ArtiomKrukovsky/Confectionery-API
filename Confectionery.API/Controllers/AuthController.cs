using Confectionery.API.Application.Commands.Authentication;
using Confectionery.API.Application.ViewModels.Authentication;
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
        public async Task<ActionResult<LogInViewModel>> LogInAsync([FromBody] LogInCommand logInCommand)
        {
            var result = await _mediator.Send(logInCommand);
            return Ok(true);
        }

        [HttpPost]
        [Route("refreshToken")]
        public async Task<ActionResult> RefreshTokenAsync([FromBody] RefreshTokenCommand refreshTokenCommand)
        {
            var result = await _mediator.Send(refreshTokenCommand);
            return Ok(result);
        }
    }
}
