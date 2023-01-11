using Confectionery.API.Application.Interfaces;
using Confectionery.API.Application.ViewModels;
using Confectionery.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using System.Runtime.Serialization;

namespace Confectionery.API.Application.Queries.Confection
{
    [DataContract]
    public class GetConfectionQuery : IQuery<ConfectionViewModel>
    {
        [DataMember]
        public Guid ConfectionId { get; set; }

        public GetConfectionQuery(Guid confectionId)
        {
            ConfectionId = confectionId;
        }
    }

    public class GetConfectionQueryHandler : IRequestHandler<GetConfectionQuery, ConfectionViewModel>
    {
        private readonly IConfectionRepository _confectionRepository;
        private readonly IMapper _mapper;

        public GetConfectionQueryHandler(IConfectionRepository confectionRepository, IMapper mapper)
        {
            _confectionRepository = confectionRepository ?? throw new ArgumentNullException(nameof(confectionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ConfectionViewModel> Handle(GetConfectionQuery request, CancellationToken cancellationToken)
        {
            var confection = await _confectionRepository.GetAsync(request.ConfectionId);

            return _mapper.Map<ConfectionViewModel>(confection);
        }
    }
}
