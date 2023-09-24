using Confectionery.API.Application.Queries.Client;
using Confectionery.API.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Confectionery.API.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("{email}")]
        public async Task<ActionResult<ClientViewModel>> GetClientAsync(string email)
        {
            var client = await _mediator.Send(new GetClientQuery(email));
            return client;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateClientAsync([FromBody] CreateClientCommand createClientCommand)
        {
            return await _mediator.Send(createClientCommand);
        }
    }
}
