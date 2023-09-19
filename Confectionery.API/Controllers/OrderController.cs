using Confectionery.API.Application.Commands.Order;
using Confectionery.API.Application.Queries.Order;
using Confectionery.API.Application.ViewModels;
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

        [HttpGet]
        public async Task<ActionResult<List<OrderDetailViewModel>>> GerOrderDetails()
        {
            var orderDetails = await _mediator.Send(new GetOrderDetailsQuery());
            return orderDetails;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateOrderAsync([FromBody] CreateOrderCommand createOrderCommand)
        {
            return await _mediator.Send(createOrderCommand);
        }
    }
}
