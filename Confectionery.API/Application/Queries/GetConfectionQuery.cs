using Confectionery.API.Application.Interfaces;
using Confectionery.API.Application.ViewModels;
using MediatR;
using System.Runtime.Serialization;

namespace Confectionery.API.Application.Queries
{
    [DataContract]
    public class GetConfectionQuery : IQuery<ConfectionViewModel>
    {
        [DataMember]
        private Guid ConfectionId { get; set; }

        public GetConfectionQuery(Guid confectionId)
        {
            ConfectionId = confectionId;
        }
    }

    public class GetConfectionQueryHandler : IRequestHandler<GetConfectionQuery, ConfectionViewModel>
    {
        public GetConfectionQueryHandler()
        {

        }

        public Task<ConfectionViewModel> Handle(GetConfectionQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
