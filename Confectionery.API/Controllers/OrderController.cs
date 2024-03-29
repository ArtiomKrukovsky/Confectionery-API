﻿using Confectionery.API.Application.Commands.Order;
using Confectionery.API.Application.Queries.Order;
using Confectionery.API.Application.ViewModels;
using Confectionery.API.Application.ViewModels.Common;
using Confectionery.Infrastructure.QueryProcessing;
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
        public async Task<ActionResult<PagedListViewModel<OrderDetailViewModel>>> GerOrderDetails([FromQuery] QueryParameters queryParameters)
        {
            var paginatedOrderDetails = await _mediator.Send(new GetOrderDetailsQuery(queryParameters));
            return paginatedOrderDetails;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateOrderAsync([FromBody] CreateOrderCommand createOrderCommand)
        {
            return await _mediator.Send(createOrderCommand);
        }
    }
}
