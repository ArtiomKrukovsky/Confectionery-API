using Confectionery.API.Application.Queries;
using Confectionery.API.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Confectionery.API.Controllers
{
    [Route("api/[controller]")]
    public class ConfectionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfectionController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<ActionResult<List<ConfectionMappingViewModel>>> GetConfectionMappingsAsync()
        {
            var confectionMappings = await _mediator.Send(new GetConfectionMappingsQuery());
            return confectionMappings;
        }

        [HttpGet]
        [Route("confectionId/{confectionId}")]
        public async Task<ActionResult<ConfectionViewModel>> GetConfectionAsync(Guid confectionId)
        {
            var confection = await _mediator.Send(new GetConfectionQuery(confectionId));
            return confection;
        }
    }
}
