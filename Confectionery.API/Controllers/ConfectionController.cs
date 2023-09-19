using Confectionery.API.Application.Queries.Confection;
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
        [Route("mappings")]
        public async Task<ActionResult<List<ConfectionMappingViewModel>>> GetConfectionMappingsAsync()
        {
            var confectionMappings = await _mediator.Send(new GetConfectionMappingsQuery());
            return confectionMappings;
        }

        [HttpGet]
        public async Task<ActionResult<List<ConfectionViewModel>>> GetConfectionsAsync()
        {
            var confections = await _mediator.Send(new GetConfectionsQuery());
            return confections;
        }

        [HttpGet]
        [Route("{confectionId}")]
        public async Task<ActionResult<ConfectionViewModel>> GetConfectionAsync(Guid confectionId)
        {
            var confection = await _mediator.Send(new GetConfectionQuery(confectionId));
            return confection;
        }

        [HttpGet]
        [Route("{confectionId}/picture")]
        public async Task<ActionResult<ConfectionPictureViewModel>> GetConfectionPictureAsync(Guid confectionId)
        {
            var confectionPicture = await _mediator.Send(new GetConfectionPictureQuery(confectionId));
            return confectionPicture;
        }
    }
}
