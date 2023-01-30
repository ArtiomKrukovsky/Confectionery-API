using Confectionery.API.Application.Commands.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Confectionery.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateOrderAsync([FromBody] CreateOrderCommand createOrderCommand)
        {
            return await _mediator.Send(createOrderCommand);
        }
    }
}
