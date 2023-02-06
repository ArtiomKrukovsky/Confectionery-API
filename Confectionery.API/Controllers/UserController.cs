using Confectionery.API.Application.Queries.User;
using Confectionery.API.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Confectionery.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("{email}")]
        public async Task<ActionResult<UserViewModel>> GetUserAsync(string email)
        {
            var user = await _mediator.Send(new GetUserQuery(email));
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUserAsync([FromBody] CreateUserCommand createUserCommand)
        {
            return await _mediator.Send(createUserCommand);
        }
    }
}
