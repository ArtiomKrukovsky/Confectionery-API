using Confectionery.API.Application.Interfaces;
using Confectionery.API.Application.ViewModels;
using MediatR;
using System.Runtime.Serialization;

namespace Confectionery.API.Application.Queries
{
    [DataContract]
    public class GetConfectionMappingsQuery : IQuery<List<ConfectionMappingViewModel>>
    {
    }

    public class GetConfectionMappingsQueryHandler : IRequestHandler<GetConfectionMappingsQuery, IEnumerable<List<ConfectionMappingViewModel>>>
    {
        public GetConfectionMappingsQueryHandler()
        {

        }

        public Task<IEnumerable<List<ConfectionMappingViewModel>>> Handle(GetConfectionMappingsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
